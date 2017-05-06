using UnityEngine;
using System.Collections;

public class Slime : Enemy {

    
    [Header("Slime settings")]

    [SerializeField]
    [Tooltip("Is slime attacking")]
    private bool m_attacking = false;


    [SerializeField]
    [Tooltip("Damage done to the player")]
    protected float m_damageWhenTouched = 3;

    [SerializeField]
    [Tooltip("Time before attacking the player again")]
    protected float m_attackingCooldown = 3f;

    
    [SerializeField]
    [Tooltip("Vertical jump force for forward jump")]
    protected float m_verticalJumpForce = 1000;

    [SerializeField]
    [Tooltip("Forward jump force for forward jump")]
    protected float m_forwardJumpForce = 1000;

    [SerializeField]
    [Tooltip("Vertical jump force for forward jump")]
    protected float m_verticalBackwardsForce = 1000;

    [SerializeField]
    [Tooltip("Forward jump force for forward jump")]
    protected float m_backwardsJumpForce = 1000;


    private float m_timeSinceLastAttack;

    private GameObject m_detectionCollider;
    private GameObject m_backCollider;



    
    // Use this for initialization
    internal override void Awake()
    {
        base.Awake();
        m_detectionCollider = gameObject.transform.FindChild("DetectionCollider").gameObject;
        m_backCollider = gameObject.transform.FindChild("BackCollider").gameObject;

	}


    void Update()
    {       
        //if (!m_attacking)
        //{
        //    if (m_rigidBody.velocity.y == 0 && !m_dead)
        //    {
        //        m_controller.Play("idle");
        //    }
        //}

        if (m_rigidBody.velocity.y == 0 && !m_dead)
        {
            m_controller.Play("idle");
        }

        //if (m_timeSinceLastAttack < 0)
        //{
        //    m_controller.Play("idle");

        //}

        m_timeSinceLastAttack -= Time.deltaTime;

    }

    /// <summary>
    /// Damage player if touched
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionStay2D(Collision2D other)
    {        
        if (m_attacking && other.gameObject.tag == "Player")
        {
            if (m_playerCombatScript == null || !m_playerCombatScript.enabled)
                m_playerCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();

            m_playerCombatScript.receiveDamage(m_damageWhenTouched, other);
            m_attacking = false;
        }

    }

    override internal void die()
    {
        base.die();

        //deactivate slime's attack 
        m_detectionCollider.SetActive(false);
        m_backCollider.SetActive(false);
      
    }

   /// <summary>
   /// El slime cuando recibe daño se echa para atras
   /// </summary>
   /// <param name="damage"></param>
    override public void receiveDamage(float damage)
    {
        base.receiveDamage(damage);

        gameObject.GetComponent<Rigidbody2D>().AddForce(-1 * gameObject.transform.right * m_backwardsJumpForce);
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, m_verticalBackwardsForce));
    }

    /// <summary>
    /// El slime cuando deteca al jugador se lanza sobre el;
    /// </summary>
    private void attackPlayer()
    {

        if (m_timeSinceLastAttack < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * m_forwardJumpForce);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, m_verticalJumpForce));
            
            
            m_controller.Play("attack");
            
            m_attacking = true;

            m_timeSinceLastAttack = m_attackingCooldown;
        }
        //print("jump to the player");

    }


    /// <summary>
    /// If it was detected from the back, the slime will turn around.
    /// If detected by front, the slime will attack player
    /// </summary>
    /// <param name="colliderName"></param>
    override public void playerDetected(string colliderName, GameObject playerGO = null)
    {

        if (colliderName == "BackCollider")
        {
            gameObject.transform.Rotate(new Vector3(0, 180));
        }
        else if (colliderName == "DetectionCollider")
        {
            attackPlayer();
        }
    }
}

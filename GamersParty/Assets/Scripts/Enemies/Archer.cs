using UnityEngine;
using System.Collections;

public class Archer : Enemy {

    
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
    protected float m_verticalJumpForce = 100;

    [SerializeField]
    [Tooltip("Forward jump force for forward jump")]
	protected float m_forwardJumpForce = 100;

    [SerializeField]
    [Tooltip("Vertical jump force for forward jump")]
	protected float m_verticalBackwardsForce = 100;

    [SerializeField]
    [Tooltip("Forward jump force for forward jump")]
	protected float m_backwardsJumpForce = 100;


    private float m_timeSinceLastAttack;

    private GameObject m_detectionCollider;
    private GameObject m_backCollider;
	private bool lookRight = true;
	private bool lookLeft = false;


    
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
	private void attackPlayer(GameObject player)
    {
		//gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, m_verticalJumpForce));

		if (lookRight) 
		{
			if (transform.position.x < player.transform.position.x - 17) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (5, 0);
			}
			else if(transform.position.x < player.transform.position.x - 15)
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			else
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-5, 0);
		} 
		else 
		{
			if (transform.position.x > player.transform.position.x + 17) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-5, 0);
			}
			else if(transform.position.x > player.transform.position.x + 15)
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			else
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (5, 0);
		}
			

        //print("jump to the player");

    }
	void OnTriggerStay2D(Collider2D coll)
	{
		
		if (coll.tag == "Player") 
		{
			if(coll.gameObject.transform.position.x <= transform.position.x)
				playerDetected ("BackCollider", coll.gameObject);
			else if(coll.gameObject.transform.position.x > transform.position.x)
				playerDetected ("DetectionCollider", coll.gameObject);
		}
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
			if (!lookLeft) 
			{
				gameObject.transform.Rotate(new Vector3(0, 180));
				lookLeft = true;
				lookRight = false;
			}
			attackPlayer (playerGO);
        }
        else if (colliderName == "DetectionCollider")
        {
			if (!lookRight) 
			{
				gameObject.transform.Rotate(new Vector3(0, 180));
				lookLeft = false;
				lookRight = true;
			}
			attackPlayer(playerGO);
        }
    }
}

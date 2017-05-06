using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour {


    protected Rigidbody2D m_RigidBody2d;
    private float m_timeSinceLastAttack;
    private GameObject m_AttackZone;
    private bool m_playerEating = false;
    private GameObject m_food;
    
    private float m_timeToFinishEating = 0f;

    [Header("PreLarva")]

    [SerializeField]
    private bool isPreLarva = false;


    [Header("Life")]
   
    [SerializeField]
    [Tooltip("The maximum life the player can have")]
    public float m_maxLife = 100.5f;

    [SerializeField]
    [Tooltip("The current life the player has")]
    public float m_currentLife = 70.4f;

    [Header("Basic Attack")]

    [SerializeField]
    [Tooltip("Amount of damage dealt by the player")]
    public float attackPower = 1f;


    [SerializeField]
    [Tooltip("Amount of time the attack is activate and is able to damage and enemy, probably linked to the animation")]
    private float attackDuration = 1f;

    [SerializeField]
    [Tooltip("Time before been able to attack again")]
    protected float m_attackingCooldown;

    [Tooltip("Prefab that instantiate when the player is hit")]
    public GameObject hit;

    [Header("Puas")]
    public bool m_isPuasActive = false;

    [Header("Activable abilities")]
     public int m_selectedAbilityNumber;


    void OnEnable()
    {
        gameObject.tag = "Player";
    }

    public float AttackPower
    {
        get
        {
            return attackPower;
        }
        set
        {
            attackPower = value;
        }
    }
    // Use this for initialization
    void Awake()
    {
        
        m_RigidBody2d = gameObject.GetComponent<Rigidbody2D>();
      
        Transform attackZoneTr = gameObject.transform.Find("AttackZone");
        if(attackZoneTr == null)
        {
            Debug.LogWarning("El objeto " + gameObject.name + " no tiene AttackZone. No pasa nada si es PreLarva");
        }else
            m_AttackZone = attackZoneTr.gameObject;


        Transform foodTr = gameObject.transform.Find("Food");
        if (foodTr == null)
        {
            Debug.LogWarning("El objeto " + gameObject.name + " no tiene Food. No pasa nada si es PreLarva");
        }
        else
            m_food = foodTr.gameObject;


    }

    void Start()
    {
     
     }

   void Update()
    {
  
        if (Input.GetButtonDown("BasicAttack") && !isPreLarva &&m_timeSinceLastAttack < 0 && !m_playerEating && !m_isPuasActive)
        {
            
            m_AttackZone.SetActive(true);

            StartCoroutine(deactiveAttackZone(attackDuration));
            m_timeSinceLastAttack = m_attackingCooldown;
            Debug.Log("ataca");

        }
  }

    IEnumerator deactiveAttackZone(float inXSeconds)
    {

        yield return new WaitForSeconds(inXSeconds);

        m_AttackZone.SetActive(false);

    }

    IEnumerator stopDamageAnim(float inXSeconds)
    {

        yield return new WaitForSeconds(inXSeconds);

        m_AttackZone.SetActive(false);

    }

    public void receiveHealth(float health)
    {
        // if the amount of life I have left is less than the amount of life I receive: My live will be full
        if ((m_maxLife - m_currentLife) < health)
            m_currentLife = m_maxLife;
        else
            m_currentLife += health;

    }

    //Take damage, si recibo daño tengo que mirar si tengo life regen para activarlo
    public void receiveDamage(float damage, Collision2D collison)
    {
        m_currentLife -= damage;

        //No mostrar vida negativas
        if (m_currentLife < 0)
            m_currentLife = 0;

        StartCoroutine(stopDamageAnim(0.5f));

        //show feedback from a hit
        if (collison != null)
        {
            Vector2 hitPoint = collison.contacts[0].point;
            Instantiate(hit, new Vector2(hitPoint.x, hitPoint.y), Quaternion.identity);
        }

        if (m_currentLife <= 0 && gameObject.tag != "DeadPlayer")
        {
            playerDies();
        }

        //Si tenemos LifeRegen activarlo
        checkLifeRegen();
    }
   
    private void checkLifeRegen()
    {
        if (gameObject.transform.gameObject.transform.Find("LifeRegen"))
            gameObject.transform.gameObject.transform.Find("LifeRegen").gameObject.SetActive(true);
    }
    private void playerDies()
    {
        print("ha muerto ");
        gameObject.tag = "DeadPlayer";

         
        gameObject.layer = LayerMask.NameToLayer("Food");

        m_food.SetActive(true);
  
        Destroy(gameObject.GetComponent<PlayerCombat>());
        Destroy(gameObject.GetComponent<PlayerMovement>());
    }

 

    public void restartPlayer()
    {
        m_currentLife = m_maxLife;
    }


}

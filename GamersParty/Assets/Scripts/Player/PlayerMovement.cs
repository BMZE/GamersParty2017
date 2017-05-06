using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    protected Rigidbody2D m_RigidBody2d;


    public bool grounded = true; //tome con el piso

    [SerializeField]
    protected bool isInWater = false;
    [SerializeField]
    protected bool canSwim = false;
    
    //Se utiliza para calculas cuanto tiempo hace desde la ultima vez que se voló
    private float m_timeSinceLastFly; 

    float speed = 20f;

    [Header("Movement Setting")]

    [SerializeField]
    [Tooltip("The max speed the player can reach when controlling the character")]
    public float m_maxSpeed = 100f;

    [SerializeField]
    [Tooltip("The amount of force apply to the player from underneath")]
    protected float m_jumpForce = 155f;

    public bool distribuidor = false;

    [Header("Fly Setting")]

    [SerializeField]
    [Tooltip("If the player can fly")]
    private bool canFly = false;

    [SerializeField]
    [Tooltip("Ventosas")]
    private bool ventosas = false;

    [SerializeField]
    [Tooltip("Time before the player can use fly force again")]
    internal float m_flyCooldown = 0.2f;


    [SerializeField]
    [Tooltip("Vertical force applied when flying")]
    internal float m_flyJumpForce = 170f;

    [Header("Falling Damage Setting")]

    [SerializeField]
    [Tooltip("Velocity the player must be falling to take damage (used in GroundCheck)")]
    internal float m_velocityThreshold = -6.5f;


    [SerializeField]
    [Tooltip("Damage taken from the fall")]
    internal float m_damageFromFall = 10f;

    [SerializeField]
    [Tooltip("If you can´t swim but you fall in the water")]
    internal float m_damageFromSwim = 10f;

    private bool moving = false;
    private bool isJumping = false;

    private Object flyStunPrefab;
    private GameObject flyStunGO;

    public bool Moving
    {
        get
        {
            return moving;
        }
        set
        {
            moving = value;
        }
    }
    public bool IsJumping
    {
        get
        {
            return isJumping;
        }
        set
        {
            isJumping = value;
        }
    }

    public bool Fly
    {
        get
        {
            return canFly;
        }
        set
        {
            canFly = value;
        }
    }
    public bool Grounded
    {
        get
        {
            return grounded;
        }
        set
        {
            grounded = value;
        }
    }
    public bool Ventosas
    {
        get
        {
            return ventosas;
        }
        set
        {
            ventosas = value;
        }
    }

    public bool Swim
    {
        get
        {
            return canSwim;
        }
        set
        {
            canSwim = value;
        }
    }

    public bool IsInWater
    {
        get
        {
            return isInWater;
        }
        set
        {
            isInWater = value;
        }
    }


    // Use this for initialization
    void Awake()
    {

        m_RigidBody2d = gameObject.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //para que salte
        //grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //para que se mueva
        float move = Input.GetAxis("Horizontal");


        m_RigidBody2d.AddForce((Vector2.right * speed) * move);
        m_RigidBody2d.velocity = new Vector2(move * m_maxSpeed, m_RigidBody2d.velocity.y);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            moving = true;
            isJumping = false;
        }
            

        //para limitar la velocidad del jugador, del otro modo no lo estaria
        if (m_RigidBody2d.velocity.x > m_maxSpeed)
        {
            
            m_RigidBody2d.velocity = new Vector2(m_maxSpeed, m_RigidBody2d.velocity.y);
        }

        if (m_RigidBody2d.velocity.x < -m_maxSpeed)
        {
            
            m_RigidBody2d.velocity = new Vector2(m_maxSpeed, m_RigidBody2d.velocity.y);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            moving = false;

    }

    //Take damage, si recibo daño tengo que mirar si tengo life regen para activarlo


    void Update()
    {

        //Debug.Log(m_RigidBody2d.velocity.y);

        if (Input.GetButtonDown("Vertical") || Input.GetKeyDown("up")) 
        {
            if (grounded || isInWater)
            {
                if(!canSwim)
                {
                    
                }
                m_RigidBody2d.AddForce(new Vector2(0, m_jumpForce));
                Debug.Log(m_RigidBody2d.velocity.y);
                isJumping = !isJumping;
                grounded = !grounded;
            }
            else
            {
                if ((canFly && m_timeSinceLastFly < 0) || (canSwim && m_timeSinceLastFly < 0))
                {
                    m_RigidBody2d.velocity = new Vector2(m_RigidBody2d.velocity.x, 0);
                    m_RigidBody2d.AddForce(new Vector2(0, m_flyJumpForce));
                    m_timeSinceLastFly = m_flyCooldown;
                    isJumping = !isJumping;

                }
                
            }
            //moving = true;
        }
        m_timeSinceLastFly -= Time.deltaTime;

        /*if (Input.GetButtonUp("Vertical") || Input.GetKeyUp("up"))
            moving = false;
            */
    }

 
}

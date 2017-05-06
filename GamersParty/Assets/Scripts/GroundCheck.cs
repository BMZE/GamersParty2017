using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {


    private PlayerMovement m_playerMovementScript;
    private PlayerCombat m_playerCombatScript;
    private Rigidbody2D m_playerRB;
    [SerializeField]
    protected bool isWater = false;
    bool ventosas = false;

    // Use this for initialization
    void Awake()
    {
        m_playerMovementScript = gameObject.GetComponentInParent<PlayerMovement>();
        m_playerCombatScript = gameObject.GetComponentInParent<PlayerCombat>();
        m_playerRB = m_playerMovementScript.gameObject.GetComponent<Rigidbody2D>();

    }



	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject.GetComponent<PlatformEffector2D>())
        {
            m_playerMovementScript.grounded = true;

            //Check for fall damage
            if (m_playerRB.velocity.y <= m_playerMovementScript.m_velocityThreshold)
            {
                //print("duele caida");
      //          m_playerCombatScript.receiveDamage(m_playerMovementScript.m_damageFromFall, null);
            }
            else
            {
                // print("no duele caida");
            }
        }
      

        if (isWater)
            m_playerMovementScript.IsInWater = true;

	}

	void OnTriggerStay2D(Collider2D col)
	{
        //si tiene un platform effector quiere deicr que es un suelo, por lo que se puede saltar
        //hay enemigos con platformeffector para que no te quedes pegados a ellos
        if(col.gameObject.GetComponent<PlatformEffector2D>() && !col.CompareTag("Enemy"))
        {
            m_playerMovementScript.grounded = true;
        }

        if (isWater)
            m_playerMovementScript.IsInWater = true;
        if(ventosas)
            m_playerMovementScript.Ventosas = true;
    }


	void OnTriggerExit2D(Collider2D col)
	{
        m_playerMovementScript.grounded = false;


        if (isWater && m_playerMovementScript.IsInWater)
            m_playerMovementScript.IsInWater = false;

    }

}

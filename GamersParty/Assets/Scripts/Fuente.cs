using UnityEngine;
using System.Collections;

public class Fuente : MonoBehaviour {


    private bool inside = false;
    private bool filled = true;
    PlayerCombat playerCombat;
    PlayerMovement playerMovement;

    [SerializeField]
    ParticleSystem particulas;
    private float timer = 0.5f;
    public int cont = 0;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            inside = true;
        
    }

	// Use this for initialization
	void Start () {

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();        
        }
    }
	
    void cooldownRegen()
    {

        playerCombat.receiveHealth(1);
        ++cont;

    }
	// Update is called once per frame
	void Update () {

        if (inside && Input.GetButtonDown("Interact"))
        {
            if (filled && playerCombat != null)
            {
                if ((playerCombat.m_currentLife < playerCombat.m_maxLife) && cont < (playerCombat.m_maxLife / 2))
                    InvokeRepeating("cooldownRegen", timer, timer);
                
            }
        }

	}

    void FixedUpdate()
    {
        if (playerMovement == null && GameObject.FindGameObjectWithTag("Player"))
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        }

        if (playerCombat == null && GameObject.FindGameObjectWithTag("Player"))
                playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();


        if (playerMovement != null)
        {
            if (playerMovement.distribuidor)
            {
                filled = true;
                particulas.enableEmission = true;
            }

            if (cont >= (playerCombat.m_maxLife / 2))
            {
                Debug.Log("cura");
                filled = !filled;
                particulas.enableEmission = false;
                playerMovement.distribuidor = false;
                CancelInvoke();
            }
            if (playerCombat.m_currentLife == playerCombat.m_maxLife)
                CancelInvoke();
        }
   

    }

    void OnTriggerExit2D(Collider2D coll)
    {
        inside = false;
        CancelInvoke();
    }
}

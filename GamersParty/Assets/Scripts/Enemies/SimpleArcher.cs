using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleArcher : MonoBehaviour {

    private GameObject m_detectionCollider;
    private GameObject m_backCollider;
    private bool lookRight = true;
    private bool lookLeft = false;
    GameObject nuevo;
    public GameObject arrow;
    float shoot;
    float frequency;
    public float hp = 50;
    // Use this for initialization
    void Start () {
        shoot = 0f;
        frequency = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void attackPlayer(GameObject player)
    {
        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, m_verticalJumpForce));
        
        if (lookRight)
        {
            if (shoot > frequency)
            {
                shoot = 0f;
                nuevo = arrow;
                Instantiate(nuevo);
                nuevo.transform.position = transform.position;
                nuevo.GetComponent<Arrow>().SetPositiveSpeed();
            }
        }
        else
        {
            if (shoot > frequency)
            {
                shoot = 0f;
                nuevo = arrow;
                Instantiate(nuevo);
                nuevo.transform.position = transform.position;
                nuevo.GetComponent<Arrow>().SetNegativeSpeed();
            }

            shoot += Time.deltaTime;
        }

        if (hp <= 0)
            Destroy(gameObject);

        //print("jump to the player");

    }
    void OnTriggerStay2D(Collider2D coll)
    {

        if (coll.tag == "Player")
        {
            if (coll.gameObject.transform.position.x <= transform.position.x)
                playerDetected("BackCollider", coll.gameObject);
            else if (coll.gameObject.transform.position.x > transform.position.x)
                playerDetected("DetectionCollider", coll.gameObject);
        }
    }

    /// <summary>
    /// If it was detected from the back, the slime will turn around.
    /// If detected by front, the slime will attack player
    /// </summary>
    /// <param name="colliderName"></param>
	public void playerDetected(string colliderName, GameObject playerGO = null)
    {
        if (colliderName == "BackCollider")
        {
            if (!lookLeft)
            {
                gameObject.transform.Rotate(new Vector3(0, 180));
                lookLeft = true;
                lookRight = false;
            }
            attackPlayer(playerGO);
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

    public int getExerience()
    {
        return 10;
    }

    public void subHP(float damage)
    {
        hp -= damage;
        
    }
}

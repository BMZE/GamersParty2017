using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float speedX;
    public GameObject Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(speedX, 0.0f, 0.0f));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerLogic>().SubHP(6);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    public void SetPositiveSpeed()
    {
        if(speedX < 0)
            speedX = -speedX;
    }

    public void SetNegativeSpeed()
    {
        if (speedX > 0)
            speedX = -speedX;
    }
}

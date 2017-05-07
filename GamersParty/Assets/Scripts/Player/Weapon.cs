using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public BoxCollider2D trigger;
	// Use this for initialization
	void Start () {
        trigger.GetComponent<BoxCollider2D>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //enables and deactivates the weapon's trigger
    public void setTrigger(bool activate)
    {
        trigger.enabled = activate;
    }

    //checks if the weapon 
    void OnTriggerEnter2D(Collider2D col)
    {
        
        
        if (trigger.enabled == true)
        {
           
            if (col.gameObject.tag == "Enemy")
            {
                GetComponentInParent<PlayerLogic>().PullTrigger(col.gameObject);
            }
        }
        else  Debug.Log("LOOOOOOOOL");

        // GetComponent<PlayerCombat2>().
        // gameObject.GetComponentInParent<ParentTrigger>().PullTrigger(c);
    }

}

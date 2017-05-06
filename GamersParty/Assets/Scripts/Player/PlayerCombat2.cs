using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat2 : MonoBehaviour {

    public GameObject weapon;
    GameObject enemyCollider;
    private float force;
    private float attack;
    private float defense;
    private float coordination;
    BoxCollider2D trigger;
    bool collisionEnemy; 

    // Use this for initialization
    void Start () {
        force = 5;
        weapon.GetComponent<Weapon>().setTrigger(false);
        collisionEnemy = false;

    }
	
	// Update is called once per frame
	void Update () {


		if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Blanca te quiero <3");
            weapon.GetComponent<Weapon>().setTrigger(true);

            if (collisionEnemy)
            {
                force = (attack * coordination);
                collisionEnemy = false;
                Debug.Log("YOU HIT AN ENEMY");
            }

        }

     

    }

    public void PullTrigger(GameObject go)
    {
        enemyCollider = go;
        collisionEnemy = true;
    }

    
}

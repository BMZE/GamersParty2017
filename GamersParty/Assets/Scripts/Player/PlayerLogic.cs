using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {


    //player stats
    public static PlayerLogic instance;
    public float attack;
    public float defense;
    public float coordination;
    public float HP;
    public int level;
    private int maxXP;
    private int experience;

	// Use this for initialization
	void Start () {
        attack = 10;
        defense = 10;
        coordination = 10;
        maxXP = 10;
        HP = 10;

        instance = this;
       
       // Debug.Log(HP);
    }
	
	// Update is called once per frame
	void Update () {
        if (experience == maxXP)
            setLevel(level + 1, 10, 5f, 2, 3, 3);

        if (HP == 0)
            Debug.Log("NOOB you are DEAD");

       // Debug.Log(HP);
    }



    //adds experience to the player
    public void AddExperience()
    {
        //experience += enemy.getExperience();
    }

    public void SubHP(float hp)
    {
        HP -= hp;
    }

    //sets the player's new level and how much experience he needs to reach the next level
    void setLevel(int newLevel, int nextLevelXP, float newHP, float newCoordination, float newAttack, float newDefense)
    {
        maxXP += nextLevelXP;
        experience = 0;
        level = newLevel;
        HP = newHP;
        Debug.Log("Well done, but you are still a NOOB");
    }

    public void SetObjectsStats(float att, float def, float cor, float hp)
    {
        attack = att;
        defense = def;
        coordination = cor;
        HP = hp;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Apple")
        {
            ObjectLogic.instance.SetObjectStats(attack, defense, coordination, HP, "Apple");
            Destroy(col.gameObject);
        }
        Debug.Log(HP);

    }
}

using UnityEngine;
using System.Collections;

public class Poisoned_gas : MonoBehaviour {

    bool vidarestada = true;
    bool inside = false;
    public float secondsBetweenDamage = 0.1f;
    public float damageToPlayer = 0.2f;

    // Use this for initialization
    PlayerCombat player;
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerCombat>();
    
    }

    public bool vidarest()
    {
        return vidarestada;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {

            inside = true;
            StartCoroutine(poison());   
        }                   
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {

            inside = false;

        }
    }

    IEnumerator poison()
    {       
        while (inside)
        {
            yield return new WaitForSeconds(secondsBetweenDamage);
            vidarestada = false;
            envenena();
            vidarestada = true;
        }
                   
    }
    void envenena()
    {
        player.receiveDamage(damageToPlayer, null);

        //player.receiveDamage(player.m_maxLife * 0.05f, null);


        //Debug.Log("quito vida");
       
        //Debug.Log(player.m_currentLife.ToString());
        //Debug.Log("vida que quito" + player.m_maxLife * 0.1f);        
        //Debug.Log("vida actual" + player.m_currentLife.ToString());        
        
        //Debug.Log("Quité vida");
    }


    //void OnTriggerStay2D(Collider2D coll)
    //{
    //    //yield return new WaitForSeconds(1);
    //    //StartCoroutine(poison());
    //}

   

}

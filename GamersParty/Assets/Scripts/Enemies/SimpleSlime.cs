using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlime : MonoBehaviour {

    public GameObject Player;
    Rigidbody2D rb;
    public Rigidbody2D rbPlayer;
    int posicion = 0; //0 Jugador se encuentra a la izquierda y 1 se encuentra a la derecha
    float time = 0.0f;
    float saltox, saltoy;
    public float frecuenciaSalto = 1.5f;
    public float hp = 30;
    private float startX;
    private float maxX;
    public float addToMax;
    bool salto;
    Animator anim;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        startX = transform.position.x;
        maxX = startX + addToMax;
        salto = false;
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
       // anim.Play("idle0");
        salto = false;
        if (transform.position.x >= Player.transform.position.x && transform.position.x >= startX - 5f)
        {
            salto = true;
            posicion = 0;
            saltox = -1.0f;
        }
        else if(transform.position.x <= Player.transform.position.x && transform.position.x <= maxX)
        {
            salto = true;
            posicion = 1;
            saltox = 1.0f;
        }
        time += Time.deltaTime;
        if (time > frecuenciaSalto && salto)
        {
            time = 0.0f;
            float p, l;

            p = Random.Range(1.0f, 1.5f);
            l = Random.Range(1.0f, 1.5f);
            Vector2 salto = new Vector2(saltox * p, 5.0f * l);
            rb.velocity = salto;
        }

        if (hp <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {

            Player.GetComponent<PlayerLogic>().SubHP(5);
            rb.AddForce(new Vector2(0.0f, 10.0f), ForceMode2D.Force);
            rbPlayer.AddForce(new Vector2(100.0f * saltox, 0.0f), ForceMode2D.Force);
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

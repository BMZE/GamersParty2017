using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{

    public float velocidadLateral;
    public float velocidadVertical;
    private bool comienzaMovimiento = false;
    private float tiempoComienzo = 0f;
    private float auxX;
    private float auxY;
    private float texture = 0;
    private float textureY = 0;
    private Vector2 position;
    GameObject pl;
    //Rigidbody2D rbodyPlayer;

    // Use this for initialization
    void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
        
        InvokeRepeating("Position", 0.05f, 0.05f);
    }

    void PersonajeEmpiezaMovimiento()
    {
        comienzaMovimiento = true;
        tiempoComienzo = Time.time;

        //el jugador se mueve
    }
    private void Position()
    {
        if(pl != null)
            position = pl.GetComponent<Rigidbody2D>().position;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (pl != null && pl.gameObject.CompareTag("Player"))
        {
            
            if (pl.GetComponent<Rigidbody2D>().velocity.x != 0)
                if (position != pl.GetComponent<Rigidbody2D>().position)
                    PersonajeEmpiezaMovimiento();
            if (comienzaMovimiento)
            {
                if (!pl.GetComponent<PlayerMovement>().IsJumping)
                    if (pl.GetComponent<Rigidbody2D>().velocity.x > 0)
                        this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(((tiempoComienzo - auxX) * velocidadLateral) + texture, this.GetComponent<Renderer>().material.mainTextureOffset.y);
                    else
                        this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(-((tiempoComienzo - auxX) * velocidadLateral) + texture, this.GetComponent<Renderer>().material.mainTextureOffset.y);

                if (pl.GetComponent<Rigidbody2D>().velocity.y > 2)
                {
                    //Debug.Log(pl.GetComponent<Rigidbody2D>().velocity.y);
                    this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(this.GetComponent<Renderer>().material.mainTextureOffset.x, ((tiempoComienzo - auxY) * velocidadVertical) + textureY);
                }
                else if (pl.GetComponent<Rigidbody2D>().velocity.y < -2)
                {
                    //Debug.Log("yipikay yeih!");
                    this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(this.GetComponent<Renderer>().material.mainTextureOffset.x, -((tiempoComienzo - auxY) * velocidadVertical) + textureY);
                }
            }
        }
        else
            pl = GameObject.FindGameObjectWithTag("Player");



    }
    void FixedUpdate()
    {

        if (pl != null && pl.gameObject.CompareTag("Player"))
        {
            if (pl.GetComponent<Rigidbody2D>().velocity.x == 0 || position == pl.GetComponent<Rigidbody2D>().position)
            {
                texture = this.GetComponent<Renderer>().material.mainTextureOffset.x;
                auxX = Time.time;
                comienzaMovimiento = false;
            }
            if (!pl.GetComponent<PlayerMovement>().IsJumping)//pl.GetComponent<Rigidbody2D>().velocity.y == 0 || position == pl.GetComponent<Rigidbody2D>().position)
            {
                textureY = this.GetComponent<Renderer>().material.mainTextureOffset.y;
                auxY = Time.time;
                comienzaMovimiento = false;
            }

        }



    }
}

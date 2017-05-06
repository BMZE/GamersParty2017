using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


    internal Animator m_controller;
    internal PlayerCombat m_playerCombatScript;
    internal LifeIndicator m_lifeIndicator;
   
    internal Rigidbody2D m_rigidBody;

    internal GameObject m_food;


    internal bool m_dead = false;

    [Header("Enemy General settings")]

    [SerializeField]
    [Tooltip("Enemy HP")]
    protected float m_enemyHP = 50f;

 
    // Use this for initialization
    internal virtual void Awake()
    {

        m_controller = gameObject.GetComponent<Animator>();

        GameObject m_playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (m_playerGameObject)
        {
            m_playerCombatScript = m_playerGameObject.GetComponent<PlayerCombat>();
        }
        
        m_lifeIndicator = gameObject.GetComponentInChildren<LifeIndicator>();

        m_rigidBody = gameObject.GetComponent<Rigidbody2D>();


        m_food = gameObject.transform.FindChild("Food").gameObject;
    }


  

    /// <summary>
    /// Enemigo recibe daño y se le baja el indicador de vida
    /// </summary>
    /// <param name="damage"></param>
    virtual public void receiveDamage(float damage)
    {
        m_enemyHP -= damage;
        m_lifeIndicator.OnDamage(damage);

        m_controller.Play("damage");

        if (m_enemyHP <= 0)
        {
            die();
        }

    }


    /// <summary>
    /// El enemigo muere, aparece el cadaver, cambia su capa para poder atraversarlo
    /// </summary>
    virtual internal void die()
    {             
        m_controller.Play("death");
        m_dead = true;
        m_food.SetActive(true);
        //Cambiar capa para que personaje pueda atravesar cadaver
        gameObject.layer = LayerMask.NameToLayer("Food");

    }

    /// <summary>
    /// Play cadaver ya comido
    /// </summary>
    internal void eaten()
    {
        m_controller.Play("corpse");

    }

    /// <summary>
    /// Devuelve vida del enemigo
    /// </summary>
    /// <returns></returns>
    public float getHp()
    {
        return m_enemyHP;
    }

    virtual public void playerDetected(string colliderName, GameObject playerGO = null)
    { 
    }
}

using UnityEngine;
using System.Collections;

public class LifeIndicator : MonoBehaviour {


    private Enemy m_EnemyScript; // The life script attach to this gameObject
    private float m_maxLife; // The maximum life of this gameObject

    private GameObject m_lifeIndicator; // The life Indicator
    private float m_initScale; // The initial scale.x of the Indicator


    void Awake()
    {

        //TODO: no hace falta poner el indicador como publico y prefab para añadir,
        //se puede buscar porque está en el mismo gameobject que el script
        m_EnemyScript = GetComponentInParent<Enemy>();
        m_maxLife = m_EnemyScript.getHp();

        m_lifeIndicator = gameObject;

        m_lifeIndicator.GetComponent<SpriteRenderer>().enabled = false;

        m_initScale = m_lifeIndicator.transform.localScale.x;
    }

    public bool OnDamage(float damage)
    {


        m_lifeIndicator.GetComponent<SpriteRenderer>().enabled = true;

        // Calculates the life percentage
        float hp = m_EnemyScript.getHp();

        if (hp <= 0)
            hp = 0;

        float lifePercent = hp / m_maxLife;

        // Scales the sprite by the lifePercent
        Vector3 indicatorScale = m_lifeIndicator.transform.localScale;
        indicatorScale.x = m_initScale * lifePercent;
        m_lifeIndicator.transform.localScale = indicatorScale;

        return true;
    }


	
}

using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{


    public Transform m_Waypoint1 = null;

    public Transform m_Waypoint2 = null;

    public float m_MovementSpeed = 30.0f;

    public float m_MinDistance = 2.0f;

    private Transform m_CurrentWaypoint = null;

    private float m_MinDistanceSqr = 0.0f;


    void Start()
    {
        m_MinDistanceSqr = m_MinDistance * m_MinDistance;

        //Se coloca al enemigo al en el waypoint1 para asegurar el punto inicial
        gameObject.transform.position = m_Waypoint1.position;
        m_CurrentWaypoint = m_Waypoint2;
    }

    void Update()
    {
        Vector3 direction = m_CurrentWaypoint.position - transform.position;
        direction.Normalize();

        transform.position += m_MovementSpeed * direction * Time.deltaTime;

        float remDist = (m_CurrentWaypoint.position - transform.position).sqrMagnitude;

        if (remDist < m_MinDistanceSqr)
        {
            if (m_CurrentWaypoint == m_Waypoint1)
                m_CurrentWaypoint = m_Waypoint2;
            else
                m_CurrentWaypoint = m_Waypoint1;
        }

    }


}


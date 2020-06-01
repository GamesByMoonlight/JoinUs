using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnCollide : MonoBehaviour
{
    public GameObject ObjectToDestroy;

    protected float m_SpawnTime;

    protected virtual void Awake()
    {
        m_SpawnTime = Time.time;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DestroyObjectOnCollide>() != null) 
        {
            if(collision.GetComponent<DestroyObjectOnCollide>().m_SpawnTime < m_SpawnTime)
            {
                Destroy(ObjectToDestroy);
            }
            return;
        }

        Destroy(ObjectToDestroy);
    }
}

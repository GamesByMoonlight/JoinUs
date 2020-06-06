using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AdjacentTileListener : MonoBehaviour
{
    public CornerstoneController ParentCornerstoneToDestroy;
    public BaseTile ParentCapstone;

    protected float m_SpawnTime;

    protected virtual void Awake()
    {
        ParentCornerstoneToDestroy = GetComponentInParent<CornerstoneController>();
        ParentCapstone = GetComponentInParent<BaseTile>();
        m_SpawnTime = Time.time;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<AdjacentTileListener>() != null) 
        {
            if(collision.GetComponent<AdjacentTileListener>().m_SpawnTime < m_SpawnTime)
            {
                Destroy(ParentCornerstoneToDestroy.gameObject);
            }
            return;
        }

        if(collision.attachedRigidbody.GetComponent<BaseTile>() != null)
        {
            AttachCaptstone(collision.attachedRigidbody.GetComponent<BaseTile>());
        }
        Destroy(ParentCornerstoneToDestroy.gameObject);
    }

    protected virtual void AttachCaptstone(BaseTile adjacentTile)
    {
        var i = ParentCornerstoneToDestroy.Index;
        var oppositeIndex = i % 2 == 0 ? 1 : -1;
        ParentCapstone.Next[i] = adjacentTile;
        adjacentTile.Next[i + oppositeIndex] = ParentCapstone;
    }
}

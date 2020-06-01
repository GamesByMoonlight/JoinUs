using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// A tile the player walks on to create a new permanent tile
/// </summary>
public class CornerstoneController : MonoBehaviour
{
    public GameObject Model;
    public int Index;   // HACK to help me more easily set direction --> 0 Top 1 Bottom 2 Right 3 Left

    protected BaseTile _parentTile;
    protected Coroutine _newTileListener;
    
    protected virtual void Awake()
    {
        _parentTile = GetComponentInParent<BaseTile>();
        Assert.IsTrue(Index >= 0 && Index <= 3, "Index should be between 0 - 3 depending on whether it is the Top, Bottom, Right or Left tile relative to its parent. Game object " + gameObject.name);
        Assert.IsNotNull(_parentTile, "Cornerstone's are expecting to be parented under BaseStones.  Is this no longer the case? Game object " + gameObject.name);
    }

    protected virtual void Start()
    {
        Model.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        CheckPlayer(collision);
        
    }

    protected virtual void CheckPlayer(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Player"))
        {
            Model.SetActive(true);
            if (_newTileListener != null)
                StopCoroutine(_newTileListener);
            _newTileListener = StartCoroutine(CreateListener());
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Player"))
        {
            Model.SetActive(false);
            StopCoroutine(_newTileListener);
        }
    }

    protected virtual IEnumerator CreateListener()
    {
        bool running = true;
        while (running)
        {
            yield return null;
            if (Input.GetButtonDown("Interact"))
            {
                TileFactory.Instance.Spawn(this);
                running = false;
                Destroy(gameObject);
            }
        }
    }
}

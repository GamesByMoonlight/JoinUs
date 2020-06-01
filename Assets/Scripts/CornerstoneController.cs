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
    public GameObject CapstonePrefab;
    public GameObject Model;
    public int Index;   // 0 Top 1 Bottom 2 Right 3 Left

    protected Coroutine _createListener;
    protected BaseStone _parentTile;
    
    protected virtual void Awake()
    {
        _parentTile = GetComponentInParent<BaseStone>();
        Assert.IsTrue(Index >= 0 && Index <= 3, "Index should be between 0 - 3 depending on whether it is the Top, Bottom, Right or Left tile relative to its parent. Game object " + gameObject.name);
        Assert.IsNotNull(_parentTile, "Cornerstone's are expecting to be parented under BaseStones.  Is this no longer the case? Game object " + gameObject.name);
    }

    protected virtual void Start()
    {
        Model.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Player"))
        {
            Model.SetActive(true);
            if (_createListener != null)
                StopCoroutine(_createListener);
            _createListener = StartCoroutine(CreateListener());
            return;
        }

        if (collision.attachedRigidbody.GetComponent<BaseStone>() != null && collision.attachedRigidbody.GetComponent<BaseStone>() != _parentTile)
        {
            var sqrMagnitude = (collision.attachedRigidbody.GetComponent<BaseStone>().transform.position - transform.position).sqrMagnitude; 
            if (sqrMagnitude > 0.1f)
                return;

            // We're on top of the colliding tile.  Remove ourselves.
            var reverseIndex = Mathf.Abs(Index%2 - 1) + Index > 1 ? 2 : 0;
            collision.attachedRigidbody.GetComponent<BaseStone>().Next[reverseIndex] = _parentTile;
            _parentTile.Next[Index] = collision.attachedRigidbody.GetComponent<BaseStone>();
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Player"))
        {
            Model.SetActive(false);
            StopCoroutine(_createListener);
        }
    }

    protected virtual IEnumerator CreateListener()
    {
        bool running = true;
        while (running)
        {
            yield return null;
            if(Input.GetButtonDown("Interact"))
            {
                Spawn();
                running = false;
                Destroy(gameObject);
            }
        }
    }

    private void Spawn()
    {
        Instantiate(CapstonePrefab, transform.position, transform.rotation);
    }
}

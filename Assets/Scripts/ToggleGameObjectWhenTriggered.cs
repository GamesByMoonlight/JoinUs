using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ToggleGameObjectWhenTriggered : MonoBehaviour
{
    public GameObject ColliderToToggle;

    protected List<ToggleGameObjectWhenTriggered> _collisions = new List<ToggleGameObjectWhenTriggered>();

    protected virtual void Awake()
    {
        Assert.IsNotNull(GetComponent<Collider2D>(), "This component is expecting to be on the same game object as its collider.  Game object " + gameObject.name);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collisions.Contains(collision.GetComponent<ToggleGameObjectWhenTriggered>()))
            return;
        
        if(collision.GetComponent<ToggleGameObjectWhenTriggered>() != null)
            _collisions.Add(collision.GetComponent<ToggleGameObjectWhenTriggered>());
        ColliderToToggle.gameObject.SetActive(false);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        Exit(collision);
    }

    protected virtual void Exit(Collider2D collision)
    {
        _collisions.Remove(collision.GetComponent<ToggleGameObjectWhenTriggered>());
        ColliderToToggle.gameObject.SetActive(true);
    }

    protected virtual void OnDestroy()
    {
        for(int i = 0; i < _collisions.Count; ++i)
        {
            Assert.IsNotNull(_collisions[i], "A collider was destroyed without notifying us.  Game object " + gameObject.name);
            _collisions[i].Exit(GetComponent<Collider2D>());
        }
    }
}

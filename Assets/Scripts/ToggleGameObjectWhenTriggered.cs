using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ToggleGameObjectWhenTriggered : MonoBehaviour
{
    public GameObject ColliderToToggle;

    protected List<Collider2D> _collisions = new List<Collider2D>();

    protected virtual void Awake()
    {
        Assert.IsNotNull(GetComponent<Collider2D>(), "This component is expecting to be on the same game object as its collider.  Game object " + gameObject.name);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collisions.Contains(collision.GetComponent<Collider2D>()))
            return;
        
        _collisions.Add(collision.GetComponent<Collider2D>());
        ColliderToToggle.gameObject.SetActive(false);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        Exit(collision);
    }

    protected virtual void Exit(Collider2D collision)
    {
        _collisions.Remove(collision.GetComponent<Collider2D>());
        if(_collisions.Count == 0)
            ColliderToToggle.gameObject.SetActive(true);
    }

    protected virtual void OnDestroy()
    {
        for(int i = 0; i < _collisions.Count; ++i)
        {
            //Assert.IsNotNull(_collisions[i], "A collider was destroyed without notifying us.  Game object " + gameObject.name);
            if(_collisions[i] != null && _collisions[i].GetComponent<ToggleGameObjectWhenTriggered>() != null)
                _collisions[i].GetComponent<ToggleGameObjectWhenTriggered>().Exit(GetComponent<Collider2D>());
        }
    }
}

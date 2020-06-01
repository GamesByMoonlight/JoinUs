using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObjectWhenTriggered : MonoBehaviour
{
    public bool EnableOnTriggerEnter = false;
    public GameObject ColliderToToggle;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        ColliderToToggle.gameObject.SetActive(EnableOnTriggerEnter);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        ColliderToToggle.gameObject.SetActive(!EnableOnTriggerEnter);
    }
}

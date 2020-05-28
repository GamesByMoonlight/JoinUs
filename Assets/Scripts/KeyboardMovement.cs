using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class KeyboardMovement : MonoBehaviour
{
    public float Speed = 10f;

    protected float _horizontal;
    protected float _vertical;

    protected virtual void FixedUpdate()
    {
        var newPos = transform.position;
        newPos.x = newPos.x + _horizontal * Speed * Time.deltaTime;
        newPos.y = newPos.y + _vertical * Speed * Time.deltaTime;
        transform.position = newPos;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }
}

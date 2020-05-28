using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FollowTarget : MonoBehaviour
{
    public Transform Target;
    
    protected virtual void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
    }
}

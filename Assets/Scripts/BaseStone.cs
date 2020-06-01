using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Intended to serve as a base class
/// </summary>
public class BaseStone : MonoBehaviour
{
    public BaseStone[] Next = new BaseStone[4];

    protected virtual void Awake()
    {
        Assert.AreEqual(4, Next.Length, "Only four directions are supported.  Ensure the Next array only has four elemnts.  Game object " + gameObject.name);
    }
}

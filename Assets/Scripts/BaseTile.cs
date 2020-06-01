using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Intended to serve as a base class
/// </summary>
public class BaseTile : MonoBehaviour
{
    public BaseTile[] Next = new BaseTile[4];
    protected CornerstoneController[] _cornerstones = new CornerstoneController[4];

    protected virtual void Awake()
    {
        InitAdjacentTiles();
        AssertionChecks();
    }

    protected virtual void InitAdjacentTiles()
    {
        _cornerstones = GetComponentsInChildren<CornerstoneController>();
    }

    protected virtual void AssertionChecks()
    {
        Assert.AreEqual(4, Next.Length, "Do not modify Next array.  Only four adjacent directions supported.  Game object " + gameObject.name);
        Assert.AreEqual(4, _cornerstones.Length, "Should start with four cornerstones on awake.  Do you have four child cornerstones?  Game object " + gameObject.name);
        for (int i = 0; i < _cornerstones.Length; ++i)
        {
            Assert.IsNotNull(_cornerstones[i], "Cornerstones incorrectly assigned.  Game object " + gameObject.name);
        }
    }
}

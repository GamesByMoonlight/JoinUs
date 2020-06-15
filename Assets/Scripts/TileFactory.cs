using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance;

    public GameObject Prefab;

    protected virtual void Awake()
    {
        Instance = this;
    }

    public virtual void Spawn(CornerstoneController controller)
    {
        Instantiate(Prefab, controller.transform.position, controller.transform.rotation);
    }
}

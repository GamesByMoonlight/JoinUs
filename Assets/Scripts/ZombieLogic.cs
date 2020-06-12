using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The logic behind the zombie movement choice
/// </summary>
public class ZombieLogic : MonoBehaviour
{
    private EntityManager entityManager;

    private GameObject closestEntity;
    private float closestDist;

    public float movementSpeed = 3f;

    private Vector3 seekVector;
    private float seekWeight = 1f;
    private Vector3 separateVector;
    private float separateWeight = .5f;

    public float viewRange = 10f;

    public GameObject turnedPrefab;

    // Start is called before the first frame update
    void Start()
    {
        entityManager = GameObject.Find("EntityManager").GetComponent<EntityManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Seek();
        Separate();
        Move();
    }

    void Seek()
    {
        FindClosest();

        if(closestDist <= viewRange)
        {
            Vector3 zPos = transform.position;
            Vector3 ePos = closestEntity.transform.position;
            Vector3 movement = new Vector3(ePos.x - zPos.x, ePos.y - zPos.y);
            seekVector = movement.normalized;
        }
        else
        {
            seekVector = Vector3.zero;
        }
    }

    void FindClosest()
    {
        closestEntity = entityManager.GetPlayer();
        closestDist = DistanceFrom(closestEntity);

        foreach (GameObject e in entityManager.GetNPCs())
        {
            if(e == null)
            {
                continue;
            }

            float dist = DistanceFrom(e);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestEntity = e;
            }
        }
    }

    float DistanceFrom(GameObject g)
    {
        Vector3 gPos = g.transform.position;
        Vector3 zPos = transform.position;
        float hypot = Mathf.Sqrt(Mathf.Pow(gPos.x - zPos.x, 2) + Mathf.Pow(gPos.y - zPos.y, 2));
        return hypot;
    }

    void Separate()
    {
        Vector3 away = Vector3.zero;
        foreach(GameObject o in entityManager.GetZombies())
        {
            if(o == null)
            {
                continue;
            }

            float dist = DistanceFrom(o);
            if(dist <= viewRange / 2)
            {
                Vector3 zPos = transform.position;
                Vector3 oPos = o.transform.position;
                Vector3 direction = zPos - oPos;
             
                away += direction / dist;
            }
        }
        separateVector = away.normalized;
    }

    void Move()
    {
        Vector3 moveVector = (seekVector * seekWeight) + (separateVector * separateWeight);
        transform.position += moveVector.normalized * movementSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}

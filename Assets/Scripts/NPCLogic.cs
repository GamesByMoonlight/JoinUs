using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLogic : MonoBehaviour
{
    private EntityManager entityManager;

    public float viewRange = 15f;
    public float movementSpeed = 5f;

    private Vector3 followVector;
    private float followWeight = 1f;
    private Vector3 runVector;
    private float runWeight = 3f;

    public GameObject turnedPrefab;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        entityManager = GameObject.Find("EntityManager").GetComponent<EntityManager>();
        player = null;
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        Run();
        Move();
    }

    void Run()
    {
        Vector3 dir = Vector3.zero;
        foreach (GameObject z in entityManager.GetZombies())
        {
            if(z == null)
            {
                continue;
            }

            float dist = DistanceFrom(z);
            if (dist <= viewRange / 2)
            {
                Vector3 nPos = transform.position;
                Vector3 zPos = z.transform.position;
                Vector3 direction = nPos - zPos;
                dir += direction / dist;
            }
        }
        runVector = dir.normalized * movementSpeed;
    }

    float DistanceFrom(GameObject g)
    {
        Vector3 gPos = g.transform.position;
        Vector3 zPos = transform.position;
        float hypot = Mathf.Sqrt(Mathf.Pow(gPos.x - zPos.x, 2) + Mathf.Pow(gPos.y - zPos.y, 2));
        return hypot;
    }

    void Follow()
    {
        followVector = Vector3.zero;
        if(player && DistanceFrom(player) < viewRange && DistanceFrom(player) > 1)
        {
            Vector3 nPos = transform.position;
            Vector3 pPos = player.transform.position;
            Vector3 movement = new Vector3(pPos.x - nPos.x, pPos.y - nPos.y);
            followVector = movement.normalized;
        }
    }

    void Move()
    {
        Vector3 moveVector = (followVector * followWeight) + (runVector * runWeight);
        transform.position += moveVector.normalized * movementSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Zombie")
        {
            entityManager.NPCTurnedZombie(gameObject);
            GameObject effect = Instantiate(turnedPrefab, collision.transform.position, Quaternion.identity) as GameObject;
            Destroy(effect.gameObject, 2.0f);
            Destroy(gameObject);
            Debug.Log(entityManager.GetZombies());
            Debug.Log(entityManager.GetNPCs());
        }

        if (collision.transform.tag == "Player")
        {
            player = collision.gameObject;
        }
    }
}

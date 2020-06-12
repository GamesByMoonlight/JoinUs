using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of the entities in the field and initiates spawning
/// </summary>
public class EntityManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject npcPrefab;

    public GameObject player;
    public List<GameObject> gravestones;
    private List<GameObject> npcs;
    private List<GameObject> zombies;

    public int NPCsSpawned = 5;
    public int percentChanceOfSpawn = 50;
    public int maxZombies = 10;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        npcs = new List<GameObject>();
        zombies = new List<GameObject>();

        elapsedTime = 0f;

        SpawnNPCs();
    }

    // Update is called once per frame
    void Update()
    {
        //code calls SpawnZombie once every second instead of every frame
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= 1f)
        {
            elapsedTime %= 1f;
            SpawnZombie();
        }
    }

    void SpawnNPCs()
    {
        for(int i = 0; i < NPCsSpawned; i++)
        {
            float x = Random.Range(-15, 15);
            float y = Random.Range(-15, 15);

            npcs.Add(Instantiate(npcPrefab, new Vector3(x, y), Quaternion.identity));
        }
    }

    void SpawnZombie()
    {
        int randNum = Random.Range(1, 100);
        if(randNum <= percentChanceOfSpawn)
        {
            int index = Random.Range(0, gravestones.Count);
            GameObject newZombie = Instantiate(zombiePrefab, gravestones[index].transform.position, Quaternion.identity);
            zombies.Add(newZombie);

            //maintain a max count of zombies in game
            if(zombies.Count > maxZombies)
            {
                GameObject tempZomb = zombies[0];
                zombies.RemoveAt(0);
                Destroy(tempZomb);
            }
        }
    }

    public void NPCTurnedZombie(GameObject npc)
    {
        GameObject newZombie = Instantiate(zombiePrefab, npc.transform.position, Quaternion.identity);
        zombies.Add(newZombie);
        npcs.Remove(npc);
    }

    public void ZombieTurnedNPC(GameObject zombie)
    {
        GameObject newNPC = Instantiate(npcPrefab, zombie.transform.position, Quaternion.identity);
        npcs.Add(newNPC);
        zombies.Remove(zombie);
    }

    public List<GameObject> GetNPCs()
    {
        return npcs;
    }

    public List<GameObject> GetZombies()
    {
        return zombies;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}

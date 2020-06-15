using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSkill : MonoBehaviour
{
    public float speedMultiplier = 1.2f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("hieeljad");
            print("hitt");
            // set player as parent object (for skill retention) 
            transform.SetParent(other.transform);

            // turn off renderer after being 'absorbed' 
            gameObject.GetComponentInChildren<Renderer>().enabled = false;

            // turn on speed up skill 
            SpeedPlayerUp();
        }
    }

    private void SpeedPlayerUp()
    {
        // increase speed 
        transform.parent.GetComponent<KeyboardMovement>().Speed *= speedMultiplier; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRepitcher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Repitcher")
        {
            var repitcher = collision.gameObject.GetComponent<Repitcher>();
            AudioEvents.current.PitchTrigger(repitcher.pitch);
            print("triggering repitch event");
        }
    }
}

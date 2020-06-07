using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRepitcher : MonoBehaviour
{
    public UnityEngine.Audio.AudioMixer mixer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Repitcher")
        {
            var repitcher = collision.gameObject.GetComponent<Repitcher>();
            mixer.SetFloat("Pitch", repitcher.pitch);
            print("triggering repitch event");
        }
    }
}

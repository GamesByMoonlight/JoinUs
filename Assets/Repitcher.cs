using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repitcher : MonoBehaviour
{
    public float pitch = 1.333f;

    public UnityEngine.Audio.AudioMixer mixer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mixer.SetFloat("Pitch", pitch);
        }
    }
}

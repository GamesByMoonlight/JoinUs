using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioToggle : MonoBehaviour
{
    private bool playing = true;
    public AudioMixer mixer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("triggered by " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            if (playing)
            {
                mixer.SetFloat(tag, -10f);
                playing = false;
            }
            else
            {
                mixer.SetFloat(tag, -0);
                playing = true;
            }
        }
    }
}

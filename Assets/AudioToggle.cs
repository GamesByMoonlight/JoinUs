using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioToggle : MonoBehaviour
{
    private const float QUIET_VOLUME = -10f;
    private const float NORMAL_VOLUME = 0;
    private bool playing = true;
    public AudioMixer mixer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playing)
            {
                mixer.SetFloat(tag, QUIET_VOLUME);
                playing = false;
            }
            else
            {
                mixer.SetFloat(tag, NORMAL_VOLUME);
                playing = true;
            }
        }
    }
}

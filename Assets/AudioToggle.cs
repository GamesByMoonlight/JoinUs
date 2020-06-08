using UnityEngine;
using UnityEngine.Audio;

public class AudioToggle : MonoBehaviour
{
    private const float QUIET_VOLUME = -10f;
    private float volume = 0;
    public AudioMixer mixer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // toggles volume of audio group between loud and quiet
        if (collision.gameObject.tag == "Player")
        {
            volume = QUIET_VOLUME - volume;
            mixer.SetFloat(tag, volume);
        }
    }
}

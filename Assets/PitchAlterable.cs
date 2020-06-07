using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchAlterable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioEvents.current.onRepitchTrigger += OnRepitch;
    }

    private void OnRepitch(float newPitch)
    {
        print("setting new pitch: " + newPitch);
        GetComponent<AudioSource>().pitch = newPitch;
    }

    private void OnDestroy()
    {
        AudioEvents.current.onRepitchTrigger -= OnRepitch;
    }
}

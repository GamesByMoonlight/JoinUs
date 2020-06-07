using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvents : MonoBehaviour
{

    public static AudioEvents current;

    // Start is called before the first frame update
    void Awake()
    {
        print("setting audio events up");
        current = this;
    }

    public event Action<float> onRepitchTrigger;
    public void PitchTrigger(float newPitch)
    {
        if (onRepitchTrigger != null)
        {
            onRepitchTrigger?.Invoke(newPitch);
            print("invoking repitch event");
        }
    }
}

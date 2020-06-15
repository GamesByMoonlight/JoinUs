using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance;

    public GameObject Prefab;
    public AudioClip[] clips; //array of clips to play
    public Text capstoneCountText;
    private int capstoneCountInt;

    private AudioSource audioSource; //actually plays the clips

    protected virtual void Awake()
    {
        Instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
        capstoneCountInt = 1;
    }

    public virtual void Spawn(CornerstoneController controller)
    {
        Instantiate(Prefab, controller.transform.position, controller.transform.rotation);
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
        capstoneCountInt++;
        capstoneCountText.text = capstoneCountInt.ToString();
    }
}

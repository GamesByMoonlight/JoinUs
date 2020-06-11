using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    // some particles to enhance the feeling of collision and beautify the scene
    public GameObject boomPrefab;
    public GameObject barsBoomPrefab;
    public GameObject padBoomPrefab;
    public GameObject percussionBoomPrefab;

    public GameObject mainCamera;

    public float cameraRescaleDuration;
    [Range(0, 1)]
    public float cameraRescaleMagnitude;

    public Color barsColor;
    public Color padColor;
    public Color percussionColor;


    // rescaling the size of camera in a short period of time to provide a shaking effect of camera
    private IEnumerator CameraRescale(float duration, float magnitude)
    {
        float originalSize = mainCamera.GetComponent<Camera>().orthographicSize;
        float timer = 0.0f;
        while (timer < duration)
        {
            float size = originalSize + Random.Range(-1.0f, 1.0f) * magnitude;
            mainCamera.GetComponent<Camera>().orthographicSize = size;

            timer += Time.deltaTime;

            yield return null;
        }
        mainCamera.GetComponent<Camera>().orthographicSize = originalSize;
    }

    // lerping the background color 
    private IEnumerator CameraColorLerp(Color color)
    {
        Color startColor = mainCamera.GetComponent<Camera>().backgroundColor;
        float t = 0.0f;
        while (t < 1)
        {
            mainCamera.GetComponent<Camera>().backgroundColor = Color.Lerp(startColor, color, t);

            t += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // when the player collides Repitchers, the main camera will shake
        if (collision.gameObject.tag == "Repitcher")
        {
            StopAllCoroutines();
            StartCoroutine(CameraRescale(cameraRescaleDuration, cameraRescaleMagnitude));
            GameObject boomEffect = Instantiate(boomPrefab, collision.transform.position, Quaternion.identity) as GameObject;
            Destroy(boomEffect.gameObject, 2.0f);
        }

        // when the player collides Bars, Pad, and Percussion, the background color of the main camera will be changed gradually
        if (collision.gameObject.tag == "Bars")
        {
            StopAllCoroutines();
            StartCoroutine(CameraColorLerp(barsColor));
            GameObject boomEffect = Instantiate(barsBoomPrefab, collision.transform.position, Quaternion.identity) as GameObject;
            Destroy(boomEffect.gameObject, 2.0f);
        }
        if (collision.gameObject.tag == "Pad")
        {
            StopAllCoroutines();
            StartCoroutine(CameraColorLerp(padColor));
            GameObject boomEffect = Instantiate(padBoomPrefab, collision.transform.position, Quaternion.identity) as GameObject;
            Destroy(boomEffect.gameObject, 2.0f);
        }
        if (collision.gameObject.tag == "Percussion")
        {
            StopAllCoroutines();
            StartCoroutine(CameraColorLerp(percussionColor));
            GameObject boomEffect = Instantiate(percussionBoomPrefab, collision.transform.position, Quaternion.identity) as GameObject;
            Destroy(boomEffect.gameObject, 2.0f);
        }
    }
}

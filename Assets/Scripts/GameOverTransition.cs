using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

/// <summary>
/// Animation transitions of the game over screen. Occurs when enabled
/// </summary>
public class GameOverTransition : MonoBehaviour
{
    [Header("Required Components")]
    public Image background;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        AssertionChecks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnEnable is called when the object becomes active
    void OnEnable()
    {
        StartCoroutine(Fade());
    }

    // Method called upon initialization. Throws any initialization errors.
    protected void AssertionChecks()
    {
        Assert.IsNotNull(background, $"Game over Background UI Object is not assigned. Game Object:{gameObject.name}");
        Assert.IsNotNull(text, $"Game over Text UI Object is not assigned. Game Object:{gameObject.name}");
    }

    // Enumerator used for lerping the transparency of the background and text.
    protected IEnumerator Fade()
    {
        // Set time before the scene is reset
        float timer = 0f;
        float duration = 1.75f;
        while (text.color.a < 1)
        {
            // Calculate the percentage in time
            timer += Time.deltaTime;
            float value = Mathf.Lerp(0, 1, timer / duration);

            // Set the alpha of the colors in relation to time
            text.color = new Color(text.color.r, text.color.g, text.color.b, value); 
            background.color = new Color(background.color.r, background.color.g, background.color.b, value);
            yield return null;
        }
    }
}

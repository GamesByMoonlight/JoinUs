using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the player's stats such as Health.
/// Also manages the player's death state and Health UI.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    public int health;

    [Header("Enemy Stats")]
    public int enemyDamage;

    [Header("Required Components")]
    public GameObject healthBar;
    public GameObject gameOverObject;
    public SpriteRenderer playerSprite;

    // Properties
    public bool IsDead { get; set; }

    private int maxHealth;
    private Vector3 origBarScale;

    // Start is called before the first frame update
    void Start()
    {
        AssertionChecks();

        maxHealth = health; // Store max health
        origBarScale = healthBar.transform.localScale; // Store the original scale
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method called upon any collision with another collider.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            TakeDamage(enemyDamage);
        }
    }

    // Method for calculating and setting the player's health after taking the input as damage.
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Check if player has died
        if (health <= 0)
        {
            IsDead = true;
            health = 0;

            StartCoroutine(Restart());
            Die();
        }

        // Update the Health Bar UI
        healthBar.transform.localScale = new Vector3(((float)health / maxHealth) * origBarScale.x, origBarScale.y, 1f);
    }

    // Method called upon initialization. Throws any initialization errors.
    protected void AssertionChecks()
    {
        Assert.IsNotNull(gameOverObject, $"Game Over UI Object is not assigned. Game Object:{gameObject.name}");
        Assert.IsNotNull(healthBar, $"Health Bar Sprite Object is not assigned. Game Object:{gameObject.name}");
        Assert.IsNotNull(playerSprite, $"Player Sprite Renderer is not assigned. Game Object:{gameObject.name}");
        Assert.AreNotEqual<int>(0, health, $"Player Health cannot be less than or equal to 0. Game Object:{gameObject.name}");
        Assert.AreNotEqual<int>(0, enemyDamage, $"Enemy Damage cannot be less than or equal to 0. Game Object:{gameObject.name}");
    }

    // Method called when the player dies. Enables the Game Over UI.
    protected void Die()
    {
        health = 0;
        gameOverObject.SetActive(true);

        // Disable all zombie scripts so nothing breaks
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach(GameObject zombie in zombies)
        {
            zombie.GetComponent<ZombieLogic>().enabled = false;
        }

        // Hide player and disable input
        playerSprite.enabled = false;
        gameObject.GetComponent<KeyboardMovement>().enabled = false;
        gameObject.GetComponent<PlayerAttackController>().enabled = false;
    }

    // Enumerator called after the player dies. Restarts the scene after a set timer.
    protected IEnumerator Restart()
    {
        // Set time before the scene is reset
        float timer = 5.0f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

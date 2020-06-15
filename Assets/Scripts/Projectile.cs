using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class handling the projectile logic
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private string enemyTagName = "Zombie";
    [SerializeField]
    private float projectileLifetime = 5.00f;

    /// <summary>
    /// Invoke the projectile despawning if it runs out of time
    /// </summary>
    private void OnEnable()
    {
        Invoke("KillProjectile", projectileLifetime);
    }

    /// <summary>
    /// When the projectile collides with the enemy, hide the projectile
    /// </summary>
    /// <param name="collision">The collider with which the collision takes place</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == enemyTagName)
        {
            CancelInvoke();
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Invokeable method to hide the projectile
    /// </summary>
    public void KillProjectile()
    {
        gameObject.SetActive(false);
    }
}

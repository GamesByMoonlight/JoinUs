using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class handling firing projectiles and object pooling
/// </summary>
public class PlayerProjectileHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> projectiles = new List<GameObject>();
    [SerializeField]
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip projectileSound = null;
    [SerializeField]
    private float projectileCooldown = 0.25f;
    [SerializeField]
    private float projectileVelocity = 2.00f;
    private bool canFireProjectile = true;
    private int projectileIndex = 0;

    /// <summary>
    /// Prevent firing projectiles if none have been assigned
    /// </summary>
    private void Start()
    {
        if(projectiles.Count == 0)
        {
            canFireProjectile = false;
        }
    }

    /// <summary>
    /// When the left mouse button is down and it is off cooldown, fire a projectile towards the cursor and play a sound effect
    /// </summary>
    private void FixedUpdate()
    {
        if(canFireProjectile && Input.GetMouseButton(0))
        {
            canFireProjectile = false;
            Invoke("ProjectileCooldownExpired", projectileCooldown);
            projectiles[projectileIndex].SetActive(true);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 direction = (mousePosition - transform.position).normalized;
            projectiles[projectileIndex].transform.position = transform.position + direction * 0.05f;
            projectiles[projectileIndex].GetComponent<Rigidbody2D>().velocity = direction * projectileVelocity;
            if(audioSource && projectileSound)
            {
                audioSource.PlayOneShot(projectileSound);
            }
            projectileIndex = (projectileIndex + 1) % projectiles.Count;
        }
    }

    /// <summary>
    /// Invokeable method to end the cooldown
    /// </summary>
    private void ProjectileCooldownExpired()
    {
        canFireProjectile = true;
    }
}

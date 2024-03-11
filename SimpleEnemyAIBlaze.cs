using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAIBlaze : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public int hitsToDestroy = 6;

    private int currentHits = -4;
    private bool canBeHit = true;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the player
            Vector3 direction = target.position - transform.position;

            // Move towards the player smoothly
            float step = followSpeed * Time.deltaTime;
            transform.Translate(direction.normalized * step, Space.World);

            // Rotate the enemy based on movement direction
            RotateEnemy(direction);
        }
    }

    private void RotateEnemy(Vector3 direction)
    {
        // Calculate the rotation angle based on the movement direction
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // Apply the rotation to the enemy
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && canBeHit)
        {
            Destroy(other.gameObject);  // Destroy the projectile

            // Increment the hit counter
            currentHits++;

            // Check if the enemy should be destroyed
            if (currentHits >= hitsToDestroy)
            {
                Destroy(gameObject);  // Destroy the enemy game object
            }
        }
    }
}
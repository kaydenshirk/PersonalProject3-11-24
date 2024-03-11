using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public float jumpForce = 15.0f;
    public bool isOnGround = true;
    public int hitsToDestroy = 3;

    private int currentHits = 0;
    private bool canBeHit = true;
    private bool isJumping = false;

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

            // Check if the player is too high to reach and if not already jumping
            if (IsPlayerTooHigh() && !isJumping)
            {
                StartCoroutine(Jump());
            }
        }
    }

    private void RotateEnemy(Vector3 direction)
    {
        // Calculate the rotation angle based on the movement direction
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // Apply the rotation to the enemy
        transform.rotation = Quaternion.Euler(-90f, angle, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && canBeHit)
        {
            Destroy(other.gameObject);  // Destroy the projectile

            // Increment the hit counter
            currentHits++;
            StartCoroutine(Cooldown());

            // Check if the enemy should be destroyed
            if (currentHits >= hitsToDestroy)
            {
                Destroy(gameObject);  // Destroy the enemy game object
            }
        }
    }

    private IEnumerator Cooldown()
    {
        canBeHit = false;
        yield return new WaitForSeconds(0.1f);  // Adjust the cooldown duration as needed
        canBeHit = true;
    }

    private bool IsPlayerTooHigh()
    {
        // Check if the player is higher than a certain threshold
        return target.position.y - transform.position.y > 1.5f;
    }

    private IEnumerator Jump()
    {
        if (isOnGround)
        { isOnGround = false;
         GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
         yield return null;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
        }
    }
}
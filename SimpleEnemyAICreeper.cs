using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAICreeper : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public float jumpForce = 15.0f;
    public bool isOnGround = true;
    public int hitsToDestroy = 3;
    public AudioSource CreeperAudio;
    public AudioClip Death;

    private int currentHits = 0;
    private bool canBeHit = true;
    private bool isJumping = false;
    [SerializeField]
    public GamePercent gamePercent;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float step = followSpeed * Time.deltaTime;
            transform.Translate(direction.normalized * step, Space.World);
            RotateEnemy(direction);

            if (IsPlayerTooHigh() && !isJumping)
            {
                StartCoroutine(Jump());
            }
        }
    }

    private void RotateEnemy(Vector3 direction)
    {
        float angle = Mathf.Atan2(-direction.x, -direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && canBeHit)
        {
            Destroy(other.gameObject);
            currentHits++;
            StartCoroutine(Cooldown());

            if (currentHits >= hitsToDestroy)
            {
                CreeperAudio.PlayOneShot(Death, 1.0f);
                Destroy(gameObject);
                gamePercent.mobsKilled++;
                gamePercent.UpdateGamePercentage();
            }
        }
    }

    private IEnumerator Cooldown()
    {
        canBeHit = false;
        yield return new WaitForSeconds(0.1f);
        canBeHit = true;
    }

    private bool IsPlayerTooHigh()
    {
        return target.position.y - transform.position.y > 2.0f;
    }

    private IEnumerator Jump()
    {
        if (isOnGround)
        {isOnGround = false;
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

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
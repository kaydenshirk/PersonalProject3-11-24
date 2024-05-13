using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAIPiglinBrute : MonoBehaviour
{

    public Transform target;
    public float followSpeed = 10f;
    public float jumpForce = 15.0f;
    public bool isOnGround = true;
    public int hitsToDestroy = 5;
    private int currentHits = 0;
    public AudioClip jump;
    private AudioSource PiglinBruteAudio;
    private bool canBeHit = true;
    // Start is called before the first frame update
    void Start()
    {
        PiglinBruteAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 direction = target.position - transform.position;

        float step = followSpeed * Time.deltaTime;
        transform.Translate(direction.normalized * step, Space.World);
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 90-angle, 0f);
        if (IsPlayerTooHigh())
        {
            StartCoroutine(Jump());
        } 
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
                Destroy(gameObject);
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
        return target.position.y - transform.position.y > 1.5f;
    }

    private IEnumerator Jump()
    {
        if (isOnGround)
        {
            isOnGround = false;
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            PiglinBruteAudio.PlayOneShot(jump, 1.0f);
            yield return new WaitForSeconds(0.1f);
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

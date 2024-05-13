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
    public Animator ZombieAnimationController;
    public SkinnedMeshRenderer zombieMesh;
    public BoxCollider ZombieColIsTrigger;
    public BoxCollider ZombieColIsNotTrigger;
    private AudioSource zombieAudio;
    public AudioClip jump;
    public AudioClip Death;
    public GameObject DeathParticles;
    private int currentHits = 0;
    private bool canBeHit = true;
    public bool isJumping = false;
    [SerializeField]
    public GamePercent gamePercent;

    void Start()
    {
        zombieAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;

        float step = followSpeed * Time.deltaTime;
        transform.Translate(direction.normalized * step, Space.World);
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
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
                    gamePercent.mobsKilled++;
                    gamePercent.UpdateGamePercentage();
                    ZombieColIsTrigger.enabled = false;
                    ZombieAnimationController.SetInteger("Health", 0);
                    followSpeed = 0f;
                    zombieAudio.PlayOneShot(Death, 1.0f);
                    StartCoroutine(WaitForDeathCoroutine());
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
            yield return new WaitForSeconds(0.1f);
            zombieAudio.PlayOneShot(jump, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
        }
    }

        private IEnumerator WaitForDeathCoroutine()
        {
          // Wait for a short duration to allow the animation to start
          yield return new WaitForSeconds(0.5f);
             while (ZombieAnimationController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
         {
             yield return null;
         }
         ZombieColIsNotTrigger.enabled = false;
         ZombieAnimationController.SetInteger("Health", 1);
         zombieMesh.enabled = false;
         DeathParticles.SetActive(true);
         yield return new WaitForSeconds(1.0f);
         Destroy(gameObject);
        }
}

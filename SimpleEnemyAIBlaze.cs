using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAIBlaze : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public int hitsToDestroy = 6;
    public Animator BlazeAnimationController;
    public BoxCollider BlazeColIsTrigger;
    public BoxCollider BlazeColIsNotTrigger;
    public GameObject DeathParticles;
    public SkinnedMeshRenderer BlazeMesh;
    public AudioSource BlazeAudio;
    public AudioClip hitSound;
    public AudioClip DeathSound;
    private int currentHits = -4;
    private bool canBeHit = true;
    [SerializeField]
    public GamePercent gamePercent;

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
            currentHits++;
            BlazeAudio.PlayOneShot(hitSound, 1.0f);

            // Check if the enemy should be destroyed
            if (currentHits >= hitsToDestroy)
            {
                gamePercent.mobsKilled++;
                gamePercent.UpdateGamePercentage();
                BlazeAnimationController.SetInteger("Health", 0);
                BlazeColIsTrigger.enabled = false;
                BlazeColIsNotTrigger.enabled = false;
                BlazeAudio.PlayOneShot(DeathSound, 1.0f);
                StartCoroutine(WaitForDeathCoroutine());
            }
        }
    }
            private IEnumerator WaitForDeathCoroutine()
             {
          yield return new WaitForSeconds(1.0f);
          while (BlazeAnimationController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
         {
             yield return null;
         }
         BlazeAnimationController.SetInteger("Health", 0);
         if (BlazeMesh != null)
            {
                BlazeMesh.enabled = false;
            }
        if (DeathParticles != null)
            {
                DeathParticles.SetActive(true);
            }

        yield return new WaitForSeconds(1.0f);
        if (gameObject != null)
            {
                Destroy(gameObject);
            }
            }
}
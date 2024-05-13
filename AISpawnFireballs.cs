using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform Blaze;
    public int projectilesPerRound = 3;
    public float roundInterval = 2.0f;
    public float spawnDelay = 0.2f; // Delay between each projectile spawn
    public AudioSource blaze;
    public AudioClip Fireball;
    public int projectilesSpawned = 0;
    public float timer = 0f;
    private GameObject[] spawnedProjectiles; // Array to store spawned projectiles

    void Start()
    {
        blaze = GetComponent<AudioSource>();
        spawnedProjectiles = new GameObject[projectilesPerRound]; // Initialize the array
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (projectilesSpawned >= projectilesPerRound)
        {
            StartCoroutine(StartCooldown());
        }

        if (timer >= roundInterval)
        {
            StartCoroutine(SpawnProjectilesSequence());
            timer = 0f;
        }
    }

    IEnumerator SpawnProjectilesSequence()
    {
        while (projectilesSpawned < projectilesPerRound)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, Blaze.position, Blaze.rotation);

            Transform projectileTransform = newProjectile.transform;

            Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();
            projectileRigidbody.velocity = transform.forward;

            projectileTransform.parent = transform;

            blaze.PlayOneShot(Fireball, 1.0f);
            spawnedProjectiles[projectilesSpawned] = newProjectile; // Store the spawned projectile in the array
            projectilesSpawned++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(1);
        timer = 0f;
        projectilesSpawned = 0;
    }

    void OnDisable()
    {
        // Deactivate all spawned projectiles
        foreach (GameObject projectile in spawnedProjectiles)
        {
            if (projectile != null) // Check if projectile is not null (in case it was destroyed)
            {
                projectile.SetActive(false);
            }
        }
    }
}

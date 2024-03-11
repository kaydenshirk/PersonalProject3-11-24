using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public int projectilesPerRound = 3; 
    public float roundInterval = 10f; 
    public float spawnDelay = 0.2f; // Delay between each projectile spawn

    private int projectilesSpawned = 0;
    private float timer = 0f;
    private bool spawning = false;

    void Update()
    {
        timer += Time.deltaTime;

        if (!spawning && projectilesSpawned >= projectilesPerRound && timer >= roundInterval)
        {
            StartCooldown();
        }
        else if (!spawning && timer >= roundInterval)
        {
            StartCoroutine(SpawnProjectilesSequence());
            timer = 0f;
        }
    }

    IEnumerator SpawnProjectilesSequence()
    {
        spawning = true;
        projectilesSpawned = 0;

        while (projectilesSpawned < projectilesPerRound)
        {
            GameObject fireball = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectilesSpawned++;
            yield return new WaitForSeconds(spawnDelay);
        }

        spawning = false;
    }

    void StartCooldown()
    {
        timer = 0f;
    }
}
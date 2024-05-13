using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public float activationDistance = 15f;
    public float groundActivationDistance = 100f;

    private GameObject[] enemies;
    private GameObject[] creepers;
    private GameObject[] brutes;
    private GameObject[] environment;
    public GameObject player;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        creepers = GameObject.FindGameObjectsWithTag("Creeper"); 
        brutes = GameObject.FindGameObjectsWithTag("PiglinBrute");        
        environment = GameObject.FindGameObjectsWithTag("ground");
    }

    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance <= activationDistance)
                {
                    enemy.SetActive(true);
                }
                if(distance >= activationDistance)
                {
                    enemy.SetActive(false);
                }
            }
        }

        foreach (GameObject creeper in creepers)
        {
            if (creeper != null)
            {
                float distance = Vector3.Distance(transform.position, creeper.transform.position);

                if (distance <= activationDistance)
                {
                    creeper.SetActive(true);
                }
                if(distance >= activationDistance)
                {
                    creeper.SetActive(false);
                }
            }
        }

        foreach (GameObject PiglinBrute in brutes)
        {
            if (PiglinBrute != null)
            {
                float distance = Vector3.Distance(transform.position, PiglinBrute.transform.position);

                if (distance <= activationDistance)
                {
                    PiglinBrute.SetActive(true);
                }
                if(distance >= activationDistance)
                {
                    PiglinBrute.SetActive(false);
                }
            }
        }

        foreach (GameObject ground in environment)
        {
            if (ground != null)
            {
                float distance = Vector3.Distance(transform.position, ground.transform.position);

                if (distance <= groundActivationDistance)
                {
                    ground.SetActive(true);
                }
                if(distance >= groundActivationDistance)
                {
                    ground.SetActive(false);
                }
            }
        }

        if (gameObject.CompareTag("EnemyProjectile"))
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance >= activationDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}

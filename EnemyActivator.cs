using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public float activationDistance = 15f;

    private GameObject[] enemies;
    public GameObject player;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
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

        if (gameObject.CompareTag("EnemyProjectile"))
        {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance >= activationDistance)
        {
            Destroy(gameObject);
            Debug.Log("this is working");
        }
        }

    }
}

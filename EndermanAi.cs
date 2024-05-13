using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndermanAi : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public int hitsToDestroy = 10;
    private int currentHits = 0;
    public float teleportRange = 20f;
    public float minDistanceToTarget = 5f; // Minimum distance to spawn from the target
    public float timer = 0f;
    public float teleportInterval = 5.0f;
    public bool isOnGround;
    [SerializeField]
    public GamePercent gamePercent;

    void Start()
    {
        TeleportRandomlyNearPlayer();
        TeleportWait();
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        float step = followSpeed * Time.deltaTime;
        transform.Translate(direction.normalized * step, Space.World);
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        timer += Time.deltaTime;

        if (timer > teleportInterval)
        {
            TeleportRandomlyNearPlayer();
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            currentHits++;

            if (currentHits >= hitsToDestroy)
            {
                Destroy(gameObject);
                gamePercent.mobsKilled++;
                gamePercent.UpdateGamePercentage();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
        }
    }

    void TeleportRandomlyNearPlayer()
    {
        StartCoroutine(TeleportRoutine());
    }

    IEnumerator TeleportRoutine()
    {
        TeleportWait();
        yield return new WaitUntil(() => isOnGround);
        
        // Calculate a random offset while ensuring it's not too close to the target
        Vector3 randomOffset;
        do
        {
            randomOffset = new Vector3(0f, 0f, Random.Range(-teleportRange, teleportRange));
        } while (Vector3.Distance(target.position + randomOffset, transform.position) < minDistanceToTarget);

        transform.position = target.position + randomOffset;
    }

    public void TeleportWait()
    {
        StartCoroutine(TeleportWaitRoutine());
    }

    IEnumerator TeleportWaitRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f); // Adjust this delay as needed
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("ground")))
            {
                transform.position = hit.point;
                break;
            }
        }
    }
}

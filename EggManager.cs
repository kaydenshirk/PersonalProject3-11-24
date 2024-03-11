using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggManager : MonoBehaviour
{
    public int eggStorage = 0;
    public int maxEggStorage = 10;
    public GameObject eggProjectilePrefab;

    // Update is called once per frame
    void Update()
    {
        // Collecting eggs when colliding with an object tagged "egg"
        if (Input.GetKeyDown(KeyCode.Z) && eggStorage > 0)
        {
            SpawnEggProjectile();
            eggStorage--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collecting eggs when colliding with an object tagged "egg"
        if (other.CompareTag("egg"))
        {
            eggStorage += 10;
            Destroy(other.gameObject);
        }
    }

    private void SpawnEggProjectile()
    {
        // Spawn an eggProjectile at the current position with no rotation
        Instantiate(eggProjectilePrefab, transform.position, Quaternion.identity);
    }
}

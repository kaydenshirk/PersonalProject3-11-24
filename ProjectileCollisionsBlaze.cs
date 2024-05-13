using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionsBlaze : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Cooldown());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>() != null && CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
        
        if(other.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
    }

    
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}

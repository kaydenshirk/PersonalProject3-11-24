using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionsBlaze : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>() != null && CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
        
        if(gameObject.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
    }
}

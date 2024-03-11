using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDestroy : MonoBehaviour
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
        
        if(gameObject.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.53f);
        Destroy(gameObject);
    }

}

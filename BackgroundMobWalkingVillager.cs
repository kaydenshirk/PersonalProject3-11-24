using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMobWalkingVillager : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public bool hasTurned = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hasTurned = false;
    }

    void Update()
    {
        if (hasTurned == false)
        {
        Vector3 movement = new Vector3(0f, 0f, speed); 
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, movement.z * speed);
        }

        if (hasTurned == true)
        {
        Vector3 movement = new Vector3(0f, 0f, speed); 
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -movement.z * speed);     
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            transform.Rotate(0, 0, 180);
            hasTurned = true;
        }
        if (collision.gameObject.CompareTag("Barrier2"))
        {
            transform.Rotate(0, 0, -180);
            hasTurned = false;
        }
    }
}

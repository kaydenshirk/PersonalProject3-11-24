using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Move the object forward relative to the player's rotation
        transform.Translate(-Vector3.up * speed * Time.deltaTime);
    }
}
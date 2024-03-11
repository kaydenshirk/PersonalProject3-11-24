using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 5.0f; 
    public Vector3 offset; 
    public float offsetWhenMovingLeft = 10.0f;
    void LateUpdate()
    {
        if (target != null)
        {
            float offsetZ;

            if (target.forward.z > 0)
            {
                offsetZ = 0.0f;
            }
            else
            {
                offsetZ = offsetWhenMovingLeft;
            }
            Vector3 desiredPosition = target.position + new Vector3(offset.x, offset.y, offset.z + offsetZ);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}

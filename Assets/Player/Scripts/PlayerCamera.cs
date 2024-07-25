using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        
        Vector3 targetPosition = transform.position + new Vector3(0, 1.5f, 0);

        targetPosition.z = cameraPosition.z;

        Camera.main.transform.position = Vector3.SmoothDamp(cameraPosition, targetPosition, ref velocity, smoothTime);
    }
}

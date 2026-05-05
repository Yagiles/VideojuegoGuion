using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // personaje
    public float smoothSpeed = 0.125f;
    public float offsetX = 0f;

    public float minX; // límite izquierdo
    public float maxX; // límite derecho

    void LateUpdate()
    {
        float desiredX = target.position.x + offsetX;
        float smoothedX = Mathf.Lerp(transform.position.x, desiredX, smoothSpeed);

        float clampedX = Mathf.Clamp(smoothedX, minX, maxX);    // limitamos la cámara

        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}


using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform target; // personaje
    public float smoothSpeed = 0.125f;
    public Vector2 offset;

    public bool followX = true; 
    public bool followY = false;

    public float minX; // limite izquierdo
    public float maxX; // limite derecho

    public float minY; 
    public float maxY;

    void LateUpdate()
    {
        float newX = transform.position.x;
        float newY = transform.position.y;

        if (followX)
        {
            float desiredX = target.position.x + offset.x;
            newX = Mathf.Lerp(transform.position.x, desiredX, smoothSpeed);
            newX = Mathf.Clamp(newX, minX, maxX);
        }

        if (followY)
        {
            float desiredY = target.position.y + offset.y;
            newY = Mathf.Lerp(transform.position.y, desiredY, smoothSpeed);
            newY = Mathf.Clamp(newY, minY, maxY);
        }

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

}

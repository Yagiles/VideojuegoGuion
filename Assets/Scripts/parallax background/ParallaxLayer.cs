using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactorX = 1f;
    public float parallaxFactorY = 1f;

    public bool useX = true;
    public bool useY = true;

    public void Move(Vector2 delta)
    {
        Vector3 newPos = transform.localPosition;

        if (useX)
            newPos.x -= delta.x * parallaxFactorX;

        if (useY)
            newPos.y -= delta.y * parallaxFactorY;

        transform.localPosition = newPos;
    }

}


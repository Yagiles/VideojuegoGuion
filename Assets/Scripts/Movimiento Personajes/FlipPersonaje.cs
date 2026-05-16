using System.Collections;
using UnityEngine;

public class FlipPersonaje : MonoBehaviour
{
    public SpriteRenderer sprite;

    public float tiempoAntesVolver = 0.4f;

    private bool ejecutando = false;

    public void HacerDobleFlip()
    {
        if (!ejecutando)
            StartCoroutine(DobleFlip());
    }

    IEnumerator DobleFlip()
    {
        ejecutando = true;

        bool flipOriginal = sprite.flipX;

        // Mira al otro lado
        sprite.flipX = !flipOriginal;

        yield return new WaitForSeconds(tiempoAntesVolver);

        // Vuelve a mirar al lado original
        sprite.flipX = flipOriginal;

        ejecutando = false;
    }
}
using System.Collections;
using UnityEngine;

public class FlipPersonaje : MonoBehaviour
{
    public SpriteRenderer sprite;

    [Header("Cantidad de flips")]
    public int numeroDeFlips = 1;

    public float tiempoEntreFlips = 0.4f;

    private bool ejecutando = false;

    public void EjecutarFlip()
    {
        if (!ejecutando)
            StartCoroutine(HacerFlips());
    }

    IEnumerator HacerFlips()
    {
        ejecutando = true;

        for (int i = 0; i < numeroDeFlips; i++)
        {
            sprite.flipX = !sprite.flipX;

            yield return new WaitForSeconds(tiempoEntreFlips);
        }

        ejecutando = false;
    }
}
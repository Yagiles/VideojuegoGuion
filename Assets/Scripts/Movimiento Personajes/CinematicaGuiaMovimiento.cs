using System.Collections;
using UnityEngine;

public class CinematicaGuiaMovimiento : MonoBehaviour
{
    public Transform personaje;

    public float distancia = 2f;
    public float velocidad = 1.5f;

    public IEnumerator MoverDerecha()
    {
        Vector3 inicio = personaje.position;

        Vector3 destino =
            inicio + Vector3.right * distancia;

        while (
            Vector3.Distance(
                personaje.position,
                destino
            ) > 0.02f
        )
        {
            personaje.position =
                Vector3.MoveTowards(
                    personaje.position,
                    destino,
                    velocidad * Time.deltaTime
                );

            yield return null;
        }

        personaje.position = destino;
    }
}
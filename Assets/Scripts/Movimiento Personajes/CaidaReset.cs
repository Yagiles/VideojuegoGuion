using UnityEngine;

public class CaidaReset : MonoBehaviour
{
    [Header("Jugador")]
    public Transform jugador;
    public Rigidbody2D rbJugador;

    [Header("Punto de respawn")]
    public Transform spawnInicio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugador.position = spawnInicio.position;

            if (rbJugador != null)
            {
                rbJugador.linearVelocity = Vector2.zero;
                rbJugador.angularVelocity = 0f;
            }
        }
    }
}
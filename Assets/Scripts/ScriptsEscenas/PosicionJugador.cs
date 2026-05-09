using UnityEngine;

public class PosicionarJugador : MonoBehaviour
{
    [Header("Guia")]
    public float distanciaGuia = 1.5f;

    void Start()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        GameObject guia = GameObject.FindGameObjectWithTag("Guia");

        GameObject spawn = GameObject.Find(DatosCambioEscena.spawnDestino);

        if (jugador != null && spawn != null)
        {
            jugador.transform.position = spawn.transform.position;
        }

        if (guia != null && spawn != null)
        {
            float direccionGuia = 1f;

            if (DatosCambioEscena.modoVuelta)
            {
                direccionGuia = -1f;
            }

            guia.transform.position = spawn.transform.position + new Vector3(direccionGuia * distanciaGuia, 0f, 0f);
        }
    }
}
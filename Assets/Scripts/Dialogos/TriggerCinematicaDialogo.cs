using System.Collections;
using UnityEngine;

public class TriggerCinematicaDialogo : MonoBehaviour
{
    public DialogoData dialogo;
    public DialogoManager dialogoManager;

    [Header("Scripts a bloquear")]
    public MonoBehaviour scriptMovimientoJugador;
    public MonoBehaviour scriptMovimientoGuia;

    [Header("Opciones")]
    public bool reactivarMovimientoGuiaAlFinal = true;

    [Header("Rigidbodies a parar")]
    public Rigidbody2D rbJugador;
    public Rigidbody2D rbGuia;

    [Header("Pantalla negra")]
    public CanvasGroup pantallaNegra;
    public float duracionFundido = 1f;

    [Header("Personajes principales")]
    public Transform jugador;
    public Transform destinoJugador;

    public Transform guia;

    [Header("Posición guía inicio cinemática")]
    public Transform posicionInicialGuiaCinematica;

    [Tooltip("Posición del guía DESPUÉS de la pantalla negra")]
    public Transform destinoGuia;

    [Header("Otros personajes")]
    public Transform[] personajes;
    public Transform[] destinosPersonajes;

    [Header("Solo una vez")]
    public bool destruirTrasActivarse = true;

    private bool activado = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activado) return;

        if (collision.CompareTag("Player"))
        {
            activado = true;

            BloquearMovimiento();

            // Colocar al guía exactamente donde quieres al INICIO de la cinemática
            if (guia != null && posicionInicialGuiaCinematica != null)
            {
                guia.position = posicionInicialGuiaCinematica.position;
            }

            // Parar velocidad después de recolocarlo
            if (rbGuia != null)
                rbGuia.linearVelocity = Vector2.zero;

            dialogoManager.alTerminarDialogo = TerminarCinematica;
            dialogoManager.IniciarDialogo(dialogo);
        }
    }

    void BloquearMovimiento()
    {
        if (scriptMovimientoJugador != null)
            scriptMovimientoJugador.enabled = false;

        if (scriptMovimientoGuia != null)
            scriptMovimientoGuia.enabled = false;

        if (rbJugador != null)
            rbJugador.linearVelocity = Vector2.zero;

        if (rbGuia != null)
            rbGuia.linearVelocity = Vector2.zero;
    }

    void TerminarCinematica()
    {
        StartCoroutine(FinalizarConFundido());
    }

    IEnumerator FinalizarConFundido()
    {
        // Fundido a negro
        if (pantallaNegra != null)
        {
            pantallaNegra.gameObject.SetActive(true);

            float tiempo = 0f;

            while (tiempo < duracionFundido)
            {
                tiempo += Time.deltaTime;
                pantallaNegra.alpha = tiempo / duracionFundido;
                yield return null;
            }

            pantallaNegra.alpha = 1f;
        }

        // Mover jugador y guía DESPUÉS de la cinemática
        if (jugador != null && destinoJugador != null)
            jugador.position = destinoJugador.position;

        if (guia != null && destinoGuia != null)
            guia.position = destinoGuia.position;

        // Mover otros personajes
        int cantidad = Mathf.Min(personajes.Length, destinosPersonajes.Length);

        for (int i = 0; i < cantidad; i++)
        {
            if (personajes[i] != null && destinosPersonajes[i] != null)
            {
                personajes[i].position = destinosPersonajes[i].position;
            }
        }

        // Parar velocidades después de moverlos
        if (rbJugador != null)
            rbJugador.linearVelocity = Vector2.zero;

        if (rbGuia != null)
            rbGuia.linearVelocity = Vector2.zero;

        // Fundido desde negro
        if (pantallaNegra != null)
        {
            float tiempo = 0f;

            while (tiempo < duracionFundido)
            {
                tiempo += Time.deltaTime;
                pantallaNegra.alpha = 1f - (tiempo / duracionFundido);
                yield return null;
            }

            pantallaNegra.alpha = 0f;
        }

        // Desbloquear movimiento
        if (scriptMovimientoJugador != null)
            scriptMovimientoJugador.enabled = true;

        if (scriptMovimientoGuia != null && reactivarMovimientoGuiaAlFinal)
            scriptMovimientoGuia.enabled = true;

        if (destruirTrasActivarse)
            Destroy(gameObject);
    }
}
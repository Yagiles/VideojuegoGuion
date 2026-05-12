using UnityEngine;

public class TriggerCinematicaDialogo : MonoBehaviour
{
    public DialogoData dialogo;
    public DialogoManager dialogoManager;

    [Header("Scripts a bloquear")]
    public MonoBehaviour scriptMovimientoJugador;
    public MonoBehaviour scriptMovimientoGuia;

    [Header("Rigidbodies a parar")]
    public Rigidbody2D rbJugador;
    public Rigidbody2D rbGuia;

    [Header("Solo una vez")]
    public bool destruirTrasActivarse = true;

    private bool activado = false;

    private float gravedadOriginalGuia;

    void Start()
    {
        if (rbGuia != null)
        {
            gravedadOriginalGuia = rbGuia.gravityScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activado) return;

        if (collision.CompareTag("Player"))
        {
            activado = true;

            BloquearMovimiento();

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
        {
            rbGuia.linearVelocity = Vector2.zero;
            rbGuia.gravityScale = 0f;
        }
    }

    void TerminarCinematica()
    {
        if (rbJugador != null)
            rbJugador.linearVelocity = Vector2.zero;

        if (rbGuia != null)
        {
            rbGuia.linearVelocity = Vector2.zero;
            rbGuia.gravityScale = gravedadOriginalGuia;
        }

        if (scriptMovimientoJugador != null)
            scriptMovimientoJugador.enabled = true;

        if (scriptMovimientoGuia != null)
            scriptMovimientoGuia.enabled = true;

        if (destruirTrasActivarse)
            Destroy(gameObject);
    }
}
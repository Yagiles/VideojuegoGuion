using UnityEngine;

public class NPCInteractuable : MonoBehaviour
{
    [Header("Identificador de este personaje")]
    public string idPersonaje;

    [Header("Diálogo")]
    public DialogoData dialogo;
    public DialogoManager dialogoManager;

    [Header("Condiciones para poder hablar")]
    public string[] idsNecesariosParaHablar;

    [Header("Movimiento a bloquear")]
    public MonoBehaviour scriptMovimientoJugador;
    public Rigidbody2D rbJugador;

    private bool jugadorCerca = false;
    private bool dialogoEnCurso = false;
    private void Update()
    {
        if (jugadorCerca && !dialogoEnCurso && Input.GetKeyDown(KeyCode.E))
        {
            IntentarHablar();
        }
    }

    void IntentarHablar()
    {
        if (dialogoManager == null) return;

        if (!CumpleCondiciones())
        {
            return;
        }

        dialogoEnCurso = true;

        BloquearMovimientoJugador();

        dialogoManager.alTerminarDialogo = TerminarDialogoNPC;
        dialogoManager.IniciarDialogo(dialogo);
    }

    bool CumpleCondiciones()
    {
        if (idsNecesariosParaHablar == null || idsNecesariosParaHablar.Length == 0)
        {
            return true;
        }

        for (int i = 0; i < idsNecesariosParaHablar.Length; i++)
        {
            if (!EstadoDialogos.instancia.HaHabladoCon(idsNecesariosParaHablar[i]))
            {
                return false;
            }
        }

        return true;
    }

    void TerminarDialogoNPC()
    {
        EstadoDialogos.instancia.MarcarComoHablado(idPersonaje);

        DesbloquearMovimientoJugador();

        dialogoEnCurso = false;
    }

    void BloquearMovimientoJugador()
    {
        if (scriptMovimientoJugador != null)
            scriptMovimientoJugador.enabled = false;

        if (rbJugador != null)
        {
            rbJugador.linearVelocity = Vector2.zero;
            rbJugador.angularVelocity = 0f;
        }
    }

    void DesbloquearMovimientoJugador()
    {
        if (rbJugador != null)
        {
            rbJugador.linearVelocity = Vector2.zero;
            rbJugador.angularVelocity = 0f;
        }

        if (scriptMovimientoJugador != null)
            scriptMovimientoJugador.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}
using System.Collections;
using UnityEngine;

public class CinematicaInicioDialogo : MonoBehaviour
{
    public DialogoData dialogoInicial;
    public DialogoManager dialogoManager;

    [Header("Guardar estado")]
    public string idCinematica = "cinematica_inicio_escena";

    [Header("Tiempo antes de empezar")]
    public float retrasoInicio = 2f;

    [Header("Scripts a bloquear durante la cinematica")]
    public MonoBehaviour scriptMovimientoJugador;
    public MonoBehaviour scriptMovimientoGuia;

    [Header("Flip al terminar")]
    public Transform personajeFlipX;

    [Header("Si la cinematica ya fue vista")]
    public Transform personajeAColocar;
    public Transform spawnFinal;

    void Start()
    {
        if (
            EstadoDialogos.instancia != null &&
            EstadoDialogos.instancia.HaHabladoCon(idCinematica)
        )
        {
            if (personajeAColocar != null && spawnFinal != null)
            {
                personajeAColocar.position = spawnFinal.position;
            }

            if (scriptMovimientoJugador != null)
                scriptMovimientoJugador.enabled = true;

            if (scriptMovimientoGuia != null)
                scriptMovimientoGuia.enabled = true;

            return;
        }

        StartCoroutine(IniciarCinematica());
    }

    IEnumerator IniciarCinematica()
    {
        BloquearMovimiento();

        yield return new WaitForSeconds(retrasoInicio);

        dialogoManager.alTerminarDialogo = TerminarCinematica;
        dialogoManager.IniciarDialogo(dialogoInicial);
    }

    void BloquearMovimiento()
    {
        if (scriptMovimientoJugador != null)
            scriptMovimientoJugador.enabled = false;

        if (scriptMovimientoGuia != null)
            scriptMovimientoGuia.enabled = false;
    }

    void TerminarCinematica()
    {
        if (
            EstadoDialogos.instancia != null &&
            !string.IsNullOrEmpty(idCinematica)
        )
        {
            EstadoDialogos.instancia.MarcarComoHablado(idCinematica);
        }

        if (scriptMovimientoJugador != null)
            scriptMovimientoJugador.enabled = true;

        if (scriptMovimientoGuia != null)
            scriptMovimientoGuia.enabled = true;

        // FLIP EN X
        if (personajeFlipX != null)
        {
            Vector3 escala = personajeFlipX.localScale;
            escala.x *= -1;
            personajeFlipX.localScale = escala;
        }
    }
}
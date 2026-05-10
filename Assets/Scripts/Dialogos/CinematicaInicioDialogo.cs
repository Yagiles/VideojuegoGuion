using System.Collections;
using UnityEngine;

public class CinematicaInicioDialogo : MonoBehaviour
{
    public DialogoData dialogoInicial;
    public DialogoManager dialogoManager;

    [Header("Tiempo antes de empezar")]
    public float retrasoInicio = 2f;

    [Header("Scripts a bloquear durante la cinemática")]
    public MonoBehaviour scriptMovimientoJugador;
    public MonoBehaviour scriptMovimientoGuia;

    void Start()
    {
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
        {
            scriptMovimientoJugador.enabled = false;
        }

        if (scriptMovimientoGuia != null)
        {
            scriptMovimientoGuia.enabled = false;
        }
    }

    void TerminarCinematica()
    {
        if (scriptMovimientoJugador != null)
        {
            scriptMovimientoJugador.enabled = true;
        }

        if (scriptMovimientoGuia != null)
        {
            scriptMovimientoGuia.enabled = true;
        }
    }
}
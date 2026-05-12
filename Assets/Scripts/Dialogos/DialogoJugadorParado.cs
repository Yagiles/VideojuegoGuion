using UnityEngine;

public class DialogoJugadorParado : MonoBehaviour
{
    public DialogoData dialogoParado1;
    public DialogoData dialogoParado2;

    public DialogoManager dialogoManager;

    [Header("Jugador")]
    public Rigidbody2D rbJugador;

    [Header("Configuracion")]
    public float tiempoParadoNecesario = 3f;
    public float velocidadMinima = 0.1f;

    private float tiempoParado = 0f;
    private int indiceDialogo = 0;

    void Update()
    {
        if (dialogoManager == null) return;
        if (dialogoManager.dialogoActivo) return;
        if (rbJugador == null) return;

        bool jugadorEstaParado = Mathf.Abs(rbJugador.linearVelocity.x) < velocidadMinima;

        if (jugadorEstaParado)
        {
            tiempoParado += Time.deltaTime;

            if (tiempoParado >= tiempoParadoNecesario)
            {
                LanzarDialogo();
                tiempoParado = 0f;
            }
        }
        else
        {
            tiempoParado = 0f;
        }
    }

    void LanzarDialogo()
    {
        if (indiceDialogo == 0)
        {
            dialogoManager.IniciarDialogo(dialogoParado1);
            indiceDialogo = 1;
        }
        else
        {
            dialogoManager.IniciarDialogo(dialogoParado2);
            indiceDialogo = 0;
        }
    }
}
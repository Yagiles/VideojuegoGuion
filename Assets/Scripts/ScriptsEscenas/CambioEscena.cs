using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public string nombreSiguienteEscena;

    [Header("Spawn")]
    public string nombreSpawnDestino;

    [Header("Guia")]
    public bool modoVueltaAlEntrar = false;

    [Header("Fundido")]
    public CanvasGroup pantallaNegra;
    public float duracionFundido = 1f;

    private bool cambiandoEscena = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cambiandoEscena) return;

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CambiarEscena());
        }
    }

    private IEnumerator CambiarEscena()
    {
        cambiandoEscena = true;

        // Guardamos dónde aparecerá el jugador en la siguiente escena
        DatosCambioEscena.spawnDestino = nombreSpawnDestino;

        // Guardamos cómo debe comportarse el guía en la siguiente escena
        DatosCambioEscena.modoVuelta = modoVueltaAlEntrar;

        float tiempo = 0f;

        while (tiempo < duracionFundido)
        {
            tiempo += Time.deltaTime;

            if (pantallaNegra != null)
            {
                pantallaNegra.alpha = tiempo / duracionFundido;
            }

            yield return null;
        }

        SceneManager.LoadScene(nombreSiguienteEscena);
    }
}
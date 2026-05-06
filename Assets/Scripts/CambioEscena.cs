using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public string nombreSiguienteEscena;
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

        float tiempo = 0f;

        while (tiempo < duracionFundido)
        {
            tiempo += Time.deltaTime;
            pantallaNegra.alpha = tiempo / duracionFundido;
            yield return null;
        }

        pantallaNegra.alpha = 1f;

        SceneManager.LoadScene(nombreSiguienteEscena);
    }
}
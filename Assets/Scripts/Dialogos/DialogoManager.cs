using System.Collections;
using UnityEngine;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    public GameObject panelDialogo;
    public TMP_Text textoNombre;
    public TMP_Text textoDialogo;
    public TMP_Text textoContinuar;

    private DialogoData dialogoActual;
    private int indiceLinea = 0;

    public bool dialogoActivo = false;
    private bool puedeAvanzar = false;

    public System.Action alTerminarDialogo;

    // maquina escribir 
    private bool escribiendo = false;
    public float velocidadEscritura = 0.03f;
    private Coroutine coroutineEscritura;

    void Start()
    {
        panelDialogo.SetActive(false);

        if (textoContinuar != null)
        {
            textoContinuar.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        /*
        if (dialogoActivo && puedeAvanzar && Input.GetKeyDown(KeyCode.E))
        {
            SiguienteLinea();
        }*/

        if (dialogoActivo && Input.GetKeyDown(KeyCode.E))
        {
            // Si está escribiendo -> completar texto
            if (escribiendo)
            {
                CompletarTexto();
            }
            // Si ya terminó -> siguiente línea
            else if (puedeAvanzar)
            {
                SiguienteLinea();
            }
        }
    }

    public void IniciarDialogo(DialogoData dialogo)
    {
        dialogoActual = dialogo;
        indiceLinea = 0;
        dialogoActivo = true;
        puedeAvanzar = false;

        panelDialogo.SetActive(true);
        MostrarLinea();

        if (textoContinuar != null)
        { 
            textoContinuar.gameObject.SetActive(true);
            textoContinuar.text = "Pulsa E para avanzar";
        }

        // StartCoroutine(ActivarAvance());
    }

    void MostrarLinea()
    {
        /*
        textoNombre.text = dialogoActual.lineas[indiceLinea].nombrePersonaje;
        textoDialogo.text = dialogoActual.lineas[indiceLinea].texto;
        */
        puedeAvanzar = false;

        textoNombre.text =
            dialogoActual.lineas[indiceLinea].nombrePersonaje;

        if (coroutineEscritura != null)
        {
            StopCoroutine(coroutineEscritura);
        }

        coroutineEscritura = StartCoroutine(EscribirTexto(dialogoActual.lineas[indiceLinea].texto));
    }

    void SiguienteLinea()
    {
        indiceLinea++;

        if (indiceLinea >= dialogoActual.lineas.Length)
        {
            TerminarDialogo();
        }
        else
        {
            MostrarLinea();
        }
    }

    void TerminarDialogo()
    {
        panelDialogo.SetActive(false);

        if (textoContinuar != null)
        {
            textoContinuar.gameObject.SetActive(false);
        }

        dialogoActual = null;
        dialogoActivo = false;
        puedeAvanzar = false;

        alTerminarDialogo?.Invoke();
        alTerminarDialogo = null;
    }
    /*
    private IEnumerator ActivarAvance()
    {
        yield return null;
        puedeAvanzar = true;
    }*/

    //añadido nuevo
    IEnumerator EscribirTexto(string texto)
    {
        escribiendo = true;

        textoDialogo.text = "";

        foreach (char letra in texto)
        {
            textoDialogo.text += letra;

            yield return new WaitForSeconds(velocidadEscritura);
        }

        escribiendo = false;
        puedeAvanzar = true;
    }
    void CompletarTexto()
    {
        if (coroutineEscritura != null)
        {
            StopCoroutine(coroutineEscritura);
        }

        textoDialogo.text =
            dialogoActual.lineas[indiceLinea].texto;

        escribiendo = false;
        puedeAvanzar = true;
    }
}
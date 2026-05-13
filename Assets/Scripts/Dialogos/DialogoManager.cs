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

    private Coroutine coroutineAutoDialogo;

    //efecto maquina escribir
    [Header("Efecto escritura")]
    public float velocidadEscritura = 0.03f;

    private bool escribiendo = false;

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
        
        if (dialogoActivo && dialogoActual != null && !dialogoActual.avanceAutomatico && Input.GetKeyDown(KeyCode.E) )
        {
            // Si está escribiendo → completar texto
            if (escribiendo)
            {
                CompletarTexto();
            }
            // Si ya terminó → avanzar
            else if (puedeAvanzar)
            {
                SiguienteLinea();
            }
        }
    }

    public void IniciarDialogo(DialogoData dialogo)
    {
        if (dialogo == null) return;

        if (coroutineAutoDialogo != null)
        {
            StopCoroutine(coroutineAutoDialogo);
            coroutineAutoDialogo = null;
        }

        dialogoActual = dialogo;
        indiceLinea = 0;
        dialogoActivo = true;
        puedeAvanzar = false;

        panelDialogo.SetActive(true);
        MostrarLinea();

        if (textoContinuar != null)
        {
            textoContinuar.gameObject.SetActive(!dialogoActual.avanceAutomatico);

            if (!dialogoActual.avanceAutomatico)
            {
                textoContinuar.text = "Pulsa E para avanzar";
            }
        }

        if (dialogoActual.avanceAutomatico)
        {
            coroutineAutoDialogo = StartCoroutine(AutoAvanzarDialogo());
        }
        else
        {
            StartCoroutine(ActivarAvance());
        }
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

        coroutineEscritura =
            StartCoroutine(
                EscribirTexto(
                    dialogoActual.lineas[indiceLinea].texto
                )
            );
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
        if (coroutineAutoDialogo != null)
        {
            StopCoroutine(coroutineAutoDialogo);
            coroutineAutoDialogo = null;
        }

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

    private IEnumerator ActivarAvance()
    {
        yield return null;
        puedeAvanzar = true;
    }

    private IEnumerator AutoAvanzarDialogo()
    {
        while (dialogoActivo)
        {
            // yield return new WaitForSeconds(dialogoActual.duracionLinea);
            while (escribiendo)
            {
                yield return null;
            }

            yield return new WaitForSeconds(dialogoActual.duracionLinea);
            // 

            if (dialogoActivo)
            {
                SiguienteLinea();
            }
        }
    }

    //añadido 
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
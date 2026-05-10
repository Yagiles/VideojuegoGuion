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
        if (dialogoActivo && puedeAvanzar && Input.GetKeyDown(KeyCode.E))
        {
            SiguienteLinea();
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

        StartCoroutine(ActivarAvance());
    }

    void MostrarLinea()
    {
        textoNombre.text = dialogoActual.lineas[indiceLinea].nombrePersonaje;
        textoDialogo.text = dialogoActual.lineas[indiceLinea].texto;
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

    private IEnumerator ActivarAvance()
    {
        yield return null;
        puedeAvanzar = true;
    }
}
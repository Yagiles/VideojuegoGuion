using System.Collections;
using UnityEngine;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    public GameObject panelDialogo;
    public TMP_Text textoNombre;
    public TMP_Text textoDialogo;

    private DialogoData dialogoActual;
    private int indiceLinea = 0;

    public bool dialogoActivo = false;
    private bool puedeAvanzar = false;

    void Start()
    {
        panelDialogo.SetActive(false);
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
        dialogoActual = null;
        dialogoActivo = false;
        puedeAvanzar = false;
    }

    private IEnumerator ActivarAvance()
    {
        yield return null;
        puedeAvanzar = true;
    }
}
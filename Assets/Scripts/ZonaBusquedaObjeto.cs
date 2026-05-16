using UnityEngine;
using TMPro;

public class ZonaBusquedaObjeto : MonoBehaviour
{
    [Header("Condicion para poder buscar")]
    public string idDialogoNecesario;

    [Header("UI")]
    public GameObject avisoBuscar;
    public TMP_Text textoResultado;

    [Header("Busqueda")]
    public bool contieneObjeto;
    public GameObject objetoAparecer;
    public Transform puntoAparicion;

    private bool jugadorDentro = false;
    private bool yaBuscado = false;

    void Start()
    {
        if (avisoBuscar != null)
            avisoBuscar.SetActive(false);

        if (objetoAparecer != null)
            objetoAparecer.SetActive(false);
    }

    void Update()
    {
        if (jugadorDentro && PuedeBuscar() && !yaBuscado && Input.GetKeyDown(KeyCode.B))
        {
            Buscar();
        }
    }

    bool PuedeBuscar()
    {
        if (string.IsNullOrEmpty(idDialogoNecesario))
            return true;

        return EstadoDialogos.instancia.HaHabladoCon(idDialogoNecesario);
    }

    void Buscar()
    {
        yaBuscado = true;

        if (avisoBuscar != null)
            avisoBuscar.SetActive(false);

        if (contieneObjeto)
        {
            if (objetoAparecer != null)
            {
                objetoAparecer.transform.position = puntoAparicion != null
                    ? puntoAparicion.position
                    : transform.position;

                objetoAparecer.SetActive(true);
            }

            if (textoResultado != null)
                textoResultado.text = "Has encontrado algo.";
        }
        else
        {
            if (textoResultado != null)
                textoResultado.text = "El objeto no se encuentra aqui.";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PuedeBuscar() && !yaBuscado)
        {
            jugadorDentro = true;

            if (avisoBuscar != null)
                avisoBuscar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorDentro = false;

            if (avisoBuscar != null)
                avisoBuscar.SetActive(false);
        }
    }
}
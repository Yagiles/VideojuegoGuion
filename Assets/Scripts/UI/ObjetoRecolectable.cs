using UnityEngine;

public class ObjetoRecolectable : MonoBehaviour
{
    public ObjetoData objetoData;
    private bool jugadorDentro = false;

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.F))
        {
            InventarioManager.Instance.AñadirObjeto(objetoData);
            FindObjectOfType<InventarioUI>().RefrescarUI();
            gameObject.SetActive(false);

            MisionData misionActual = MisionManager.Instance.GetMisionActual();
            if(misionActual != null && misionActual.objetoRequerido == objetoData)
            {
                MisionManager.Instance.CompletarMisionActual();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            jugadorDentro = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            jugadorDentro = false;
    }
}
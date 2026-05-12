using UnityEngine;

public class ObjetoRecolectable : MonoBehaviour
{
    public ObjetoData objetoData;
    private bool jugadorDentro = false;

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            InventarioManager.Instance.AñadirObjeto(objetoData);
            Debug.Log("Recogido: " + objetoData.nombreObjeto);
            gameObject.SetActive(false);
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
using UnityEngine;

public class DialogoTrigger : MonoBehaviour
{
    public DialogoData dialogo;
    public DialogoManager dialogoManager;

    private bool jugadorDentro = false;

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            // Solo inicia diálogo si no hay uno ya activo
            if (!dialogoManager.dialogoActivo)
            {
                dialogoManager.IniciarDialogo(dialogo);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorDentro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorDentro = false;
        }
    }
}
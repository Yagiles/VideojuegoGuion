using UnityEngine;

public class DialogoTrigger : MonoBehaviour
{
    [Header("Diálogos")]
    public DialogoData dialogo;                  // Dialogo normal (se repite)
    public DialogoData dialogoEspecial;          // Dialogo con el objeto

    [Header("Objeto requerido")]
    public bool requiereObjeto = false;          // Activa o no la comprobación
    public ObjetoData objetoRequerido;                // Nombre exacto del objeto requerido

    [Header("Objeto que se activa al terminar el diálogo normal")]
    public GameObject objetoAActivar;
    private bool objetoYaActivado = false;  // ← añadir esto

    [Header("Referencias")]
    public DialogoManager dialogoManager;

    private bool jugadorDentro = false;
    private bool dialogoEspecialYaVisto = false; // Opcional: para no repetirlo

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogoManager.dialogoActivo)
            {
                IniciarDialogoSegunEstado();
            }
        }
    }

    void IniciarDialogoSegunEstado()
    {
        // Si este NPC no requiere objeto, comportamiento normal
        if (!requiereObjeto)
        {
            if (objetoAActivar != null && !objetoYaActivado)  // ← comprobar aquí
            {
                dialogoManager.alTerminarDialogo += ActivarObjeto;
            }
            dialogoManager.IniciarDialogo(dialogo);
            return;
        }

        // Si requiere objeto, comprobamos el inventario
        if (InventarioManager.Instance.TieneObjeto(objetoRequerido))
        {
            dialogoManager.IniciarDialogo(dialogoEspecial);
        }
        else
        {
            dialogoManager.IniciarDialogo(dialogo);
        }
    }

    void ActivarObjeto()
    {
        objetoAActivar.SetActive(true);
        objetoYaActivado = true;  // ← marcar como activado
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
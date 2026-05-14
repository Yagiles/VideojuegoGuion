using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventarioUI : MonoBehaviour
{
    [Header("Slots (arrastrar los 4 Images)")]
    public Image[] iconosSlots = new Image[4];
    public Sprite spriteVacio;

    [Header("Popup descripción")]
    public GameObject panelPopup;
    public TMP_Text textoNombrePopup;
    public TMP_Text textoDescripcionPopup;

    private KeyCode[] teclas = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Busca el CanvasInventario y hazlo persistente también
        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas == null)
        {
            // Si InventarioUI no contiene el canvas, búscalo por nombre
            GameObject canvasObj = GameObject.Find("CanvasInventario");
            if (canvasObj != null)
                DontDestroyOnLoad(canvasObj);
        }
    }

    void Update()
    {
        for (int i = 0; i < teclas.Length; i++)
        {
            if (Input.GetKeyDown(teclas[i]))
                ManejarTecla(i);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && panelPopup.activeSelf)
            panelPopup.SetActive(false);
    }

    void ManejarTecla(int slot)
    {
        ObjetoData objeto = InventarioManager.Instance.GetObjeto(slot);

        if (objeto == null)
        {
            panelPopup.SetActive(false);
            return;
        }

        if (panelPopup.activeSelf && textoNombrePopup.text == objeto.nombreObjeto)
        {
            panelPopup.SetActive(false);
            return;
        }

        textoNombrePopup.text = objeto.nombreObjeto;
        textoDescripcionPopup.text = objeto.descripcion;
        panelPopup.SetActive(true);
    }

    public void RefrescarUI()
    {
        for (int i = 0; i < iconosSlots.Length; i++)
        {
            ObjetoData objeto = InventarioManager.Instance.GetObjeto(i);
            if (objeto != null)
                iconosSlots[i].sprite = objeto.icono;
            else
                iconosSlots[i].sprite = null;
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MisionManager : MonoBehaviour
{
    public static MisionManager Instance;

    [Header("Lista de misiones en orden")]
    public List<MisionData> misiones;

    private int indiceMisionActual = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnScenaCargada;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnScenaCargada;
    }

    void OnScenaCargada(Scene escena, LoadSceneMode mode)
    {
        if (indiceMisionActual >= misiones.Count) return;

        MisionData misionActual = misiones[indiceMisionActual];

        if (misionActual.seCompletaAlCambiarEscena)
        {
            // Comprueba si ya estamos en la escena correcta para mostrar la siguiente mision
            ActualizarUI();
        }
    }

    public void CompletarMisionActual()
    {
        if (indiceMisionActual >= misiones.Count) return;

        indiceMisionActual++;
        ActualizarUI();
    }

    public MisionData GetMisionActual()
    {
        if (indiceMisionActual >= misiones.Count) return null;
        return misiones[indiceMisionActual];
    }

    void ActualizarUI()
    {
        MisionUI ui = FindFirstObjectByType<MisionUI>();
        if (ui != null)
            ui.RefrescarUI();
    }
}
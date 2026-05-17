using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MisionManager : MonoBehaviour
{
    public static MisionManager Instance;

    [Header("Lista de misiones en orden")]
    public List<MisionData> misiones;

    private int indiceMisionActual = 0;
    private bool primeraEscena = true;

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
        // Ignora la primera carga
        if (primeraEscena)
        {
            primeraEscena = false;
            ActualizarUI();
            return;
        }

        if (indiceMisionActual >= misiones.Count) return;

        MisionData misionActual = misiones[indiceMisionActual];

        if (misionActual.seCompletaAlCambiarEscena)
            CompletarMisionActual();
        else
            ActualizarUI();
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
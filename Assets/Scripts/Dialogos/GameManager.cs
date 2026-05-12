using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<string> inventario = new List<string>();

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

    public void AñadirObjeto(string nombreObjeto)
    {
        if (!inventario.Contains(nombreObjeto)) // Evita duplicados
            inventario.Add(nombreObjeto);
    }

    public bool TieneObjeto(string nombreObjeto)
    {
        return inventario.Contains(nombreObjeto);
    }
}
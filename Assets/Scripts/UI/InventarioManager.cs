using UnityEngine;
using System.Collections.Generic;

public class InventarioManager : MonoBehaviour
{
    public static InventarioManager Instance;

    private List<ObjetoData> objetos = new List<ObjetoData>();

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

    public void AñadirObjeto(ObjetoData objeto)
    {
        if (!objetos.Contains(objeto))
        {
            objetos.Add(objeto);
            Debug.Log("Objeto añadido: " + objeto.nombreObjeto);
        }
    }

    public bool TieneObjeto(ObjetoData objeto)
    {
        return objetos.Contains(objeto);
    }

    public List<ObjetoData> GetObjetos()
    {
        return objetos;
    }
}
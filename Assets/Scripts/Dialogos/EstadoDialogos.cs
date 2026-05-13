using System.Collections.Generic;
using UnityEngine;

public class EstadoDialogos : MonoBehaviour
{
    public static EstadoDialogos instancia;

    private HashSet<string> personajesHablados = new HashSet<string>();

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarcarComoHablado(string idPersonaje)
    {
        personajesHablados.Add(idPersonaje);
    }

    public bool HaHabladoCon(string idPersonaje)
    {
        return personajesHablados.Contains(idPersonaje);
    }
}
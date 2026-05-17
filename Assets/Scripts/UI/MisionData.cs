using UnityEngine;

[CreateAssetMenu(fileName = "NuevaMision", menuName = "Misiones/Mision")]
public class MisionData : ScriptableObject
{
    public string tituloMision;
    [TextArea(2, 4)]
    public string descripcion;
    public bool seCompletaAlCambiarEscena = true;
    public ObjetoData objetoRequerido; // Solo si se completa recogiendo un objeto
}
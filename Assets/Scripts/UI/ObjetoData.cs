using UnityEngine;

[CreateAssetMenu(fileName = "NuevoObjeto", menuName = "Inventario/Objeto")]
public class ObjetoData : ScriptableObject
{
    public string nombreObjeto;
    [TextArea(2, 4)]
    public string descripcion;
    public Sprite icono;
}
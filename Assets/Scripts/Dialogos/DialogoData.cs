using UnityEngine;

[System.Serializable]
public class LineaDialogo
{
    public string nombrePersonaje;

    [TextArea(2, 5)]
    public string texto;
}

[CreateAssetMenu(fileName = "NuevoDialogo", menuName = "Dialogos/Dialogo")]
public class DialogoData : ScriptableObject
{
    public LineaDialogo[] lineas;
}
using System.Collections;
using UnityEngine;

public class MoverRey : MonoBehaviour
{
    private float cantidadMoverse = 0.07f;
    private NPCInteractuable npcInteractuable;
    private void Awake()
    {
        npcInteractuable = GetComponent<NPCInteractuable>();
    }
    void Update()
    {
        if (npcInteractuable.dialogoTerminado)
        {
            StartCoroutine(Moverse());
        }
    }

    public IEnumerator Moverse()
    {
        float tiempoMoverse = 120f;
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoMoverse)
        {
            transform.Translate(Vector3.right * cantidadMoverse * Time.deltaTime);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
    }
}

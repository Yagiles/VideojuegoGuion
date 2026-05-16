using System.Collections;
using UnityEngine;

public class MoverRey : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 1.5f;
    public float tiempoMoverse = 5f;

    [Header("Personajes adicionales")]
    public Transform juglar;

    private NPCInteractuable npcInteractuable;
    private bool movimientoIniciado = false;

    private void Awake()
    {
        npcInteractuable = GetComponent<NPCInteractuable>();
    }

    void Update()
    {
        if (npcInteractuable == null) return;

        if (npcInteractuable.dialogoTerminado && !movimientoIniciado)
        {
            movimientoIniciado = true;
            StartCoroutine(Moverse());
        }
    }

    IEnumerator Moverse()
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < tiempoMoverse)
        {
            Vector3 movimiento = Vector3.right * velocidad * Time.deltaTime;

            transform.Translate(movimiento);

            if (juglar != null)
                juglar.Translate(movimiento);

            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
    }
}
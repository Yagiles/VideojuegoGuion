using System.Collections;
using UnityEngine;

public class BesoPersonajes : MonoBehaviour
{
    [Header("Personajes")]
    public Transform personajeA;
    public Transform personajeB;

    [Header("Inclinacion")]
    public float inclinacionA = 15f;
    public float inclinacionB = -15f;

    [Header("Tiempo")]
    public float duracionInclinar = 0.4f;
    public float tiempoBeso = 0.5f;
    public float duracionVolver = 0.4f;

    private bool ejecutando = false;

    public void EmpezarBeso()
    {
        if (!ejecutando)
            StartCoroutine(AnimarBeso());
    }

    IEnumerator AnimarBeso()
    {
        ejecutando = true;

        Quaternion rotInicialA = personajeA.rotation;
        Quaternion rotInicialB = personajeB.rotation;

        Quaternion rotBesoA = Quaternion.Euler(0, 0, inclinacionA);
        Quaternion rotBesoB = Quaternion.Euler(0, 0, inclinacionB);

        float tiempo = 0f;

        while (tiempo < duracionInclinar)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracionInclinar;

            personajeA.rotation = Quaternion.Lerp(rotInicialA, rotBesoA, t);
            personajeB.rotation = Quaternion.Lerp(rotInicialB, rotBesoB, t);

            yield return null;
        }

        personajeA.rotation = rotBesoA;
        personajeB.rotation = rotBesoB;

        yield return new WaitForSeconds(tiempoBeso);

        tiempo = 0f;

        while (tiempo < duracionVolver)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracionVolver;

            personajeA.rotation = Quaternion.Lerp(rotBesoA, rotInicialA, t);
            personajeB.rotation = Quaternion.Lerp(rotBesoB, rotInicialB, t);

            yield return null;
        }

        personajeA.rotation = rotInicialA;
        personajeB.rotation = rotInicialB;

        ejecutando = false;
    }
}

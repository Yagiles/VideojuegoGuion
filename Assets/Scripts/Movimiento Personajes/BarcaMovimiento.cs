using UnityEngine;

public class BarcaMovimiento : MonoBehaviour
{
    [Header("Puntos")]
    public Transform puntoA;
    public Transform puntoB;

    [Header("Movimiento")]
    public float velocidad = 2f;

    private Transform objetivoActual;

    private void Start()
    {
        objetivoActual = puntoB;
    }

    private void Update()
    {
        if (puntoA == null || puntoB == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            objetivoActual.position,
            velocidad * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, objetivoActual.position) < 0.05f)
        {
            objetivoActual = objetivoActual == puntoA ? puntoB : puntoA;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
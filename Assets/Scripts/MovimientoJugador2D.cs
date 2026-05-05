using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador2D : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 8f;

    private float movimientoX;
    private Rigidbody2D rb;
    private bool enSuelo;

    public Transform detectorSuelo;
    public float radioSuelo = 0.2f;
    public LayerMask capaSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detectar si está en el suelo
        enSuelo = Physics2D.OverlapCircle(detectorSuelo.position, radioSuelo, capaSuelo);
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        rb.linearVelocity = new Vector2(movimientoX * velocidad, rb.linearVelocity.y);
    }

    public void OnMove(InputValue value)
    {
        movimientoX = value.Get<Vector2>().x;
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed) return;

        // Solo salta si está en el suelo
        if (enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }
}
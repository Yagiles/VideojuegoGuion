using UnityEngine;

public class GuiaMovimiento : MonoBehaviour
{
    private Transform jugador;
    private Rigidbody2D rb;
    private Rigidbody2D rbJugador;

    [Header("Movimiento guia")]
    public float velocidad = 6f;
    public float distanciaDelante = 2f;
    public float margenParada = 0.1f;

    [Header("Direccion del camino")]
    public int direccionObjetivo = 1; // 1 derecha, -1 izquierda

    [Header("Flotacion")]
    public float alturaFlotacion = 0.25f;
    public float velocidadFlotacion = 2f;

    private float posicionInicialY;

    [Header("Patrulla futura")]
    public bool usarPatrulla = false;
    public Transform puntoA;
    public Transform puntoB;
    private Vector2 objetivoActual;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject objJugador = GameObject.FindGameObjectWithTag("Player");

        if (objJugador != null)
        {
            jugador = objJugador.transform;
            rbJugador = objJugador.GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.LogError("No se encontro ningun objeto con tag Player");
        }

        posicionInicialY = transform.position.y;

        if (puntoB != null)
            objetivoActual = puntoB.position;
    }

    void FixedUpdate()
    {
        Flotar();

        if (usarPatrulla)
        {
            Patrullar();
        }
        else
        {
            IrEnDireccionObjetivo();
        }
    }

    void IrEnDireccionObjetivo()
    {
        if (jugador == null || rbJugador == null) return;

        float velocidadJugadorX = rbJugador.linearVelocity.x;

        if (Mathf.Abs(velocidadJugadorX) < 0.1f)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        float posicionMinimaDelante = jugador.position.x + (direccionObjetivo * distanciaDelante);

        bool guiaEstaDelante;

        if (direccionObjetivo == 1)
            guiaEstaDelante = transform.position.x >= posicionMinimaDelante;
        else
            guiaEstaDelante = transform.position.x <= posicionMinimaDelante;

        if (guiaEstaDelante)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(direccionObjetivo * velocidad, rb.linearVelocity.y);
        }
    }

    void Flotar()
    {
        float nuevaY = posicionInicialY + Mathf.Sin(Time.time * velocidadFlotacion) * alturaFlotacion;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
    }

    void Patrullar()
    {
        if (puntoA == null || puntoB == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float direccion = Mathf.Sign(objetivoActual.x - transform.position.x);
        rb.linearVelocity = new Vector2(direccion * velocidad, rb.linearVelocity.y);

        if (Vector2.Distance(transform.position, objetivoActual) < 0.1f)
        {
            if (objetivoActual == (Vector2)puntoA.position)
                objetivoActual = puntoB.position;
            else
                objetivoActual = puntoA.position;
        }
    }
}
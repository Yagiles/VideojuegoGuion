using UnityEngine;

public class GuiaMovimiento : MonoBehaviour
{
    private Transform jugador;
    private Rigidbody2D rb;
    private Rigidbody2D rbJugador;

    [Header("Movimiento guia")]
    public float velocidad = 10f;
    public float distanciaDelante = 6f;
    public float margenParada = 0.1f;

    [Header("Direccion del camino")]
    public int direccionObjetivo = 1; // 1 derecha, -1 izquierda

    [Header("Flotacion")]
    public float alturaSobreSuelo = 1.5f;
    public float alturaFlotacion = 0.2f;
    public float velocidadFlotacion = 2f;
    public float suavizadoVertical = 0.15f;
    public float alturaMaximaSobreJugador = 3f;

    [Header("Deteccion suelo")]
    public LayerMask capaSuelo;
    public float distanciaRaycastAbajo = 8f;
    public float distanciaDeteccionDelante = 2f;

    private float velocidadVerticalSuavizada;

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
    }

    void FixedUpdate()
    {
        IrEnDireccionObjetivo();
        AjustarAlturaSobreSuelo();
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

    void AjustarAlturaSobreSuelo()
    {
        float sueloY = BuscarSueloMasAlto();

        if (sueloY == Mathf.NegativeInfinity)
            return;

        float flotacion = Mathf.Sin(Time.time * velocidadFlotacion) * alturaFlotacion;
        float objetivoY = sueloY + alturaSobreSuelo + flotacion;

        // Limite para que el guia no se vaya demasiado arriba
        float alturaMaxima = jugador.position.y + alturaMaximaSobreJugador;
        objetivoY = Mathf.Min(objetivoY, alturaMaxima);

        float diferenciaY = objetivoY - rb.position.y;
        float velocidadY = diferenciaY / suavizadoVertical;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocidadY);
    }

    float BuscarSueloMasAlto()
    {
        float sueloMasAlto = Mathf.NegativeInfinity;

        Vector2 origenCentro = rb.position;
        Vector2 origenDelante = rb.position + new Vector2(direccionObjetivo * distanciaDeteccionDelante, 0f);

        sueloMasAlto = Mathf.Max(sueloMasAlto, RaycastSuelo(origenCentro));
        sueloMasAlto = Mathf.Max(sueloMasAlto, RaycastSuelo(origenDelante));

        return sueloMasAlto;
    }

    float RaycastSuelo(Vector2 origen)
    {
        Vector2 origenAlto = origen + Vector2.up * distanciaRaycastAbajo * 0.5f;

        RaycastHit2D hit = Physics2D.Raycast(
            origenAlto,
            Vector2.down,
            distanciaRaycastAbajo,
            capaSuelo
        );

        if (hit.collider != null)
            return hit.point.y;

        return Mathf.NegativeInfinity;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(
            transform.position + Vector3.up * distanciaRaycastAbajo * 0.5f,
            transform.position + Vector3.down * distanciaRaycastAbajo * 0.5f
        );

        Vector3 posicionDelante = transform.position + new Vector3(direccionObjetivo * distanciaDeteccionDelante, 0f, 0f);

        Gizmos.DrawLine(
            posicionDelante + Vector3.up * distanciaRaycastAbajo * 0.5f,
            posicionDelante + Vector3.down * distanciaRaycastAbajo * 0.5f
        );
    }
}
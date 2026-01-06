using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    public int vidaMaxima = 8;
    private int vidaActual;
    public float velocidadMovimiento = 3f;
    public float distanciaMinima = 5f;
    private Transform jugador;
    public GameObject esferaPrefab;
    public Transform puntoDisparo;
    public float velocidadProyectil = 20f;
    public float cadenciaDisparo = 2f;
    private float tiempoUltimoDisparo;
    public float rangoDisparo = 20f; // Renombrado para mayor claridad

    private void Start()
    {
        vidaActual = vidaMaxima;
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        tiempoUltimoDisparo = Time.time;
        
        if (puntoDisparo == null)
        {
            GameObject punto = new GameObject("PuntoDisparo");
            punto.transform.SetParent(transform);
            punto.transform.localPosition = Vector3.forward * 0.5f;
            puntoDisparo = punto.transform;
            Debug.Log("PuntoDisparo creado automáticamente");
        }
    }

    private void Update()
    {
        // El enemigo SIEMPRE persigue al jugador
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);
        
        // Calcular dirección hacia el jugador
        Vector3 direccion = (jugador.position - transform.position).normalized;
        direccion.y = 0;
        
        // Rotar hacia el jugador
        if (direccion != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f);
        }

        // Moverse hacia el jugador (pero detenerse a distancia mínima)
        if (distanciaAlJugador > distanciaMinima)
        {
            transform.position += direccion * velocidadMovimiento * Time.deltaTime;
        }

        // Disparar solo si está dentro del rango de disparo
        if (distanciaAlJugador <= rangoDisparo && Time.time >= tiempoUltimoDisparo + cadenciaDisparo)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    private void Disparar()
    {
        GameObject esfera = Instantiate(esferaPrefab, puntoDisparo.position, Quaternion.identity);
        
        Rigidbody rb = esfera.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = esfera.AddComponent<Rigidbody>();
        }

        rb.isKinematic = false;
        rb.useGravity = false;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.constraints = RigidbodyConstraints.None;

        Vector3 direccionDisparo = (jugador.position - puntoDisparo.position).normalized;
        
        rb.AddForce(direccionDisparo * velocidadProyectil, ForceMode.VelocityChange);

        Destroy(esfera, 5f);
    }

    public void RecibirDanio(int cantidad = 1)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDisparo);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaMinima);
        
        if (puntoDisparo != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(puntoDisparo.position, 0.3f);
        }
    }
}
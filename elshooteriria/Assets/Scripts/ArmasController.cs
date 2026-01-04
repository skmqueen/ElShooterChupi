using UnityEngine;
using System.Collections;


public class ArmasController : MonoBehaviour
{
    public float rangoDisparo = 200;
    public LayerMask area;
    private Transform camaraPlayer;
    public GameObject disparoMancha;
    public float retroceso = 4f;
    public float velocidadRetorno = 10f; 
    private Vector3 posicionInicial; 
    private Quaternion rotacionInicial;
    public GameObject flash;
    public Transform pitorro;
    public float disparoVelocidad;
    public int maxBalas = 8;
    private float ultimoDisparo = Mathf.NegativeInfinity;
    public int balasActuales;
    public float tiempoRecarga = 2f;
    private bool recargando = false;


    private void Awake()
    {
        balasActuales = maxBalas;
    }



    private void Start()
    {
            camaraPlayer= GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    private void Update ()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
        IntentoDisparo();
        Retroceso(); 
        VolverAPosicion();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Recarga());    
        }
    }

    private bool IntentoDisparo()
{
    if (recargando) return false;

    if (ultimoDisparo + disparoVelocidad < Time.time)
    {
        if (balasActuales > 0)
        {
            Disparo();
            balasActuales--;
            return true;
        }
    }
    return false;
}

    private void Disparo()
    {
        GameObject flashDos = Instantiate(flash, pitorro.position, Quaternion.Euler(pitorro.forward), transform);
        Destroy(flashDos, 1f);

        Retroceso();
        RaycastHit hit;
        if (Physics.Raycast(camaraPlayer.position, camaraPlayer.forward, out hit, rangoDisparo, area))
        {
            Debug.Log("PIUM");
            GameObject mancha = Instantiate(disparoMancha, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(mancha, 4f);

            // NUEVO: Detectar si le dimos a un enemigo
            EnemigoIA enemigo = hit.collider.GetComponent<EnemigoIA>();
            if (enemigo != null)
            {
                enemigo.RecibirDanio(1);
            }
        }

        ultimoDisparo = Time.time;
    }

        private void Retroceso()
        {
        float divisionRetroceso = 50f; 
        transform.localPosition = transform.forward * (retroceso / divisionRetroceso); 
        transform.localRotation *= Quaternion.Euler(2f, 0f, 0f);
        }

        private void VolverAPosicion() 
        { transform.localPosition = Vector3.Lerp(transform.localPosition, posicionInicial, Time.deltaTime * velocidadRetorno); 
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotacionInicial, Time.deltaTime * velocidadRetorno); 
        }

IEnumerator Recarga()
{
    recargando = true;
    float tiempoTranscurrido = 0f;
    float duracionAnimacion = tiempoRecarga * 0.3f; // Solo usa 30% del tiempo total
    
    while (tiempoTranscurrido < duracionAnimacion)
    {
        tiempoTranscurrido += Time.deltaTime;
        float progreso = tiempoTranscurrido / duracionAnimacion;
        
        // Movimiento vertical: abajo y arriba en una sola curva
        float movimiento = Mathf.Sin(progreso * Mathf.PI) * -0.3f;
        
        transform.localPosition = posicionInicial + new Vector3(0f, movimiento, 0f);
        
        yield return null;
    }
    
    transform.localPosition = posicionInicial;
    
    balasActuales = maxBalas;
    recargando = false;
}



}

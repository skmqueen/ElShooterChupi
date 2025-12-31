using UnityEngine;

//Agregamos Character Controller
[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public Camera cameraPrincipal;
    public float velocidadJugador = 5f;
    public float saltoJugador = 2f;
    public float gravedad = -20f;
    public float velocidadCorrer = 10f; 
    public float sensibilidadRotacion = 200f;

    private float anguloVertCamara;

    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    CharacterController characterController;

    private void Awake () {
        characterController = GetComponent<CharacterController>();
    }


    void Start()
    {
        
    }

    void Update()
    {
        Movimiento();
        Mirar();
    }

 private void Movimiento()
{
    if (characterController.isGrounded)
    {
        if (moveInput.y < 0)
        {
            moveInput.y = -2f;
        }
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.z = Input.GetAxis("Vertical");
        moveInput = transform.TransformDirection(moveInput);
        moveInput.x *= velocidadJugador;
        moveInput.z *= velocidadJugador;

        if (Input.GetButton("Run"))
        {
            moveInput = transform.TransformDirection(moveInput) * velocidadCorrer;
        }
        else
        {
            moveInput= transform.TransformDirection(moveInput) * velocidadJugador;
        }

        if (Input.GetButtonDown("Jump"))
        {
            moveInput.y = Mathf.Sqrt(saltoJugador * -2f * gravedad);
        }
    }

    moveInput.y += gravedad * Time.deltaTime;
    characterController.Move(moveInput * Time.deltaTime);
}

private void Mirar()
{
    rotationInput.x = Input.GetAxis("Mouse X") * sensibilidadRotacion * Time.deltaTime;
    rotationInput.y = Input.GetAxis("Mouse Y") * sensibilidadRotacion * Time.deltaTime;
    
    anguloVertCamara = anguloVertCamara + rotationInput.y;
    anguloVertCamara = Mathf.Clamp (anguloVertCamara, -70, 70);

    transform.Rotate(Vector3.up * rotationInput.x);
    cameraPrincipal.transform.localRotation = Quaternion.Euler(-anguloVertCamara, 0f, 0f);
}


}

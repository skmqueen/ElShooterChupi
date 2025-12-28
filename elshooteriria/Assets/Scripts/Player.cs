using UnityEngine;

//Agregamos Character Controller
[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{

    public float velocidadJugador = 5f;
    public float saltoJugador = 2f;
    public float gravedad = -20f;

    Vector3 moveInput = Vector3.zero;
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

        if (Input.GetButtonDown("Jump"))
        {
            moveInput.y = Mathf.Sqrt(saltoJugador * -2f * gravedad);
        }
    }

    moveInput.y += gravedad * Time.deltaTime;
    characterController.Move(moveInput * Time.deltaTime);
}



}

using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    private Quaternion rotacionOriginal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotacionOriginal = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Seguimiento();
    }

    private void Seguimiento()
    {

    //Estas funciones hacen un pequeño delay para que el arma no se mueva al mismo tiempo que el personaje

    float t_xLookInput = Input.GetAxis("Mouse X");
    float t_yLookInput = Input.GetAxis("Mouse Y");

    Quaternion t_xAngleAdjustment = Quaternion.AngleAxis(-t_xLookInput *1.45f, Vector3.up);
    Quaternion t_yAngleAdjustment = Quaternion.AngleAxis(t_yLookInput *1.45f, Vector3.right);
    Quaternion t_targerRotation = rotacionOriginal * t_xAngleAdjustment * t_yAngleAdjustment;

    transform. localRotation = Quaternion. Lerp(transform. localRotation, t_targerRotation, Time.deltaTime * 10f);
        }
}

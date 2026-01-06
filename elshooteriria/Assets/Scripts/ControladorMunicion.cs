using UnityEngine;
using TMPro;

public class ControladorMunicion : MonoBehaviour
{
    public TextMeshProUGUI textoMunicion;

    public void ActualizarMunicion(int balasActuales, int balasMaximas)
    {
        if (textoMunicion != null)
        {
            textoMunicion.text = "Munición: " + balasActuales + "/" + balasMaximas;
        }
        if (balasActuales == 0)
        {
            textoMunicion.text = "Recarga";
        }
    }
}
using UnityEngine;
using TMPro;

public class ControladorVidas : MonoBehaviour
{
    public TextMeshProUGUI textoVidas;

    public void ActualizarVidas(int vidasActuales, int vidasMaximas)
    {
        if (textoVidas != null)
        {
            textoVidas.text = "Vidas: " + vidasActuales + " / " + vidasMaximas;
        }
    }
}
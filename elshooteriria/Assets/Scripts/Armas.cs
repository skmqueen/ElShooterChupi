using UnityEngine;
using System.Collections.Generic;


public class Armas : MonoBehaviour
{
    public List<ArmasController> startingArmas = new List<ArmasController>();
    public Transform armaSocket;
    public Transform defaultSocket;
    public Transform aimSocket;

    public int armaActiva { get; private set; }

    private ArmasController[] armasSlots = new ArmasController[2];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        armaActiva = -1;

        foreach (ArmasController startingArma in startingArmas)
        {
            AnadirArma(startingArma);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
private void AnadirArma(ArmasController armaPrefab)
{
    armaSocket = defaultSocket;

    for (int i = 0; i < armasSlots.Length; i++)
    {
        if (armasSlots[i] == null)
        {
            ArmasController armaClone = Instantiate(armaPrefab, armaSocket);
            armaClone.transform.localPosition = Vector3.zero;
            armaClone.transform.localRotation = Quaternion.identity;

            armasSlots[i] = armaClone;

            // Si es la primera arma, activarla
            if (armaActiva == -1)
            {
                armaActiva = i;
                armaClone.gameObject.SetActive(true);
            }
            else
            {
                armaClone.gameObject.SetActive(false);
            }

            return;
        }
    }
}

    }


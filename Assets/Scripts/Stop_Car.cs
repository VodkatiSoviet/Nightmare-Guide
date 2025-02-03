using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop_Car : MonoBehaviour
{
    public static Stop_Car instance;

    public bool stop=false;

    private void Start()
    {
        if (instance == null) { instance = this; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car")){
            stop = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car")){
            stop = false;
        }
    }
}

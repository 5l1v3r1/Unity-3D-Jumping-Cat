using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuncuInput : MonoBehaviour {

    Oyuncu oyuncu;

    void Start () {
        // Oyuncu component
        oyuncu = GetComponent<Oyuncu>();
    }
	
	void Update () {

        // Eğer zıplama tuşuna basarsam zıplama metodunu çağır.
        //  || Input.GetTouch(0).tapCount == 1
        if (Input.GetButtonDown("Jump"))
        {
            // Oyuncu zıplama metodunu getir
            oyuncu.Ziplama();
        }
        else if (Input.GetButtonDown("Jump") && !oyuncu.IsBariyer && oyuncu.IsDoubleZiplama)
        {
            // Oyuncu double zıplama metodunu getir
            oyuncu.DoubleZiplama();
        }

    }
}

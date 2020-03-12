using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasarVer : MonoBehaviour {

    public int Hasar;
    Oyuncu oyuncu;

    private void Start()
    {
        oyuncu = FindObjectOfType<Oyuncu>();
    }
    

    /// <summary>
    /// Temas edildiği zaman
    /// </summary>
    /// <param name="obje"></param>
    private void OnTriggerEnter2D(Collider2D obje)
    {
        if(obje.tag == "Oyuncu")
        {
            // Oyuncuya zarar ver
            oyuncu.isOyuncuZarar = true;
        }
    }

    /// <summary>
    /// Temas devam ederken
    /// </summary>
    /// <param name="obje"></param>
    private void OnTriggerStay2D(Collider2D obje)
    {
        if (obje.tag == "Oyuncu")
        {
            // Oyuncuya zarar ver
            oyuncu.isOyuncuZarar = true;
        }
    }

    /// <summary>
    ///  Temastan kurtulduğu zaman
    /// </summary>
    /// <param name="obje"></param>
    private void OnTriggerExit2D(Collider2D obje)
    {
        if (obje.tag == "Oyuncu")
        {
            // Oyuncuya zarar ver
            oyuncu.isOyuncuZarar = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelleriUret : MonoBehaviour {

    [Tooltip("Üretilecek levelin prefabsı eklenecek")]
    public GameObject LevelPrefabs;

    [Tooltip("Oluşturulacak level sayısı")]
    public int LevelSayisi;

    float LevelBoyuY;

	void Start ()
    {
        // Level oluştur
        LevelOlustur();
    }
    

    void LevelOlustur()
    {
        for (int i = 1; i < LevelSayisi; i++)
        {
            // Levelin boyu
            LevelBoyuY = (Mathf.Abs(transform.position.y) * (2 * i)) + 0.3f;

            // Yeni Level Pozisyonu
            Vector3 LevelPozisyonu = new Vector3(0, LevelBoyuY, 0);

            // Bariyeri oluştur
            GameObject Level = Instantiate(
                LevelPrefabs,
                LevelPozisyonu,
                Quaternion.identity
                );
            Level.transform.parent = transform;
        }
        
    }



}

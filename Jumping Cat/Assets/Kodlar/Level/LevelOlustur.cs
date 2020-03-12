using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOlustur : MonoBehaviour {

    [Tooltip ("Bariyer Prefabslar")]
    public GameObject[] Bariyerler;
    [Tooltip("Spawn edilecek bariyerlerin noktaları")]
    public Transform[] SpawnNoktalari;

    // Bariyer Listesi
    GameObject BariyerList;

    private void Start()
    {
        // Bariyer Listesini bul
        BariyerList = GameObject.Find("Bariyerler");
        
        // Bariyerleri Spawnla
        RastgeleBariyer();
    }
    
    // Otomatik Bariyer Ekler
    private void RastgeleBariyer()
    {
        // Bariyerleri döndür
        for (int i = 0; i < SpawnNoktalari.Length; i++)
        {
            // Rastgele bariyer seç. Scale ve Posizyonlarını al
            int BariyerIndex = Random.Range(0, Bariyerler.Length);
            GameObject Bariyer = Bariyerler[BariyerIndex];
            Vector3 Bariyer_Scale = Bariyer.transform.localScale;
            Vector3 Bariyer_Position = Bariyer.transform.position;

            // Spawn noktası seç. Scale ve Posizyonlarını al
            int SpawnNoktasiIndex = i;
            GameObject SpawnBolgesi = SpawnNoktalari[SpawnNoktasiIndex].gameObject;
            Vector3 Spawn_Scale = SpawnBolgesi.transform.localScale;
            Vector3 Spawn_Position = SpawnBolgesi.transform.position;

            // Bariyer ve Spawn noktasının X scale değerlerini karşılaştır
            // Eğer Bariyer Spawn noktasından küçükse oluştur
            if (Bariyer_Scale.x < Spawn_Scale.x)
            {
                // Yeni bir spawn pozisyonu üret
                Vector3 Yeni_SpawnPozisyonu = Spawn_Position;
                var md = SpawnBolgesi.GetComponent<MeshRenderer>();
                var x1 = SpawnBolgesi.transform.position.x - md.bounds.size.x / 2;
                var x2 = SpawnBolgesi.transform.position.x + md.bounds.size.x / 2;

                //Debug.Log(x1 + " - " + x2);

                if(x1 < -2f)
                {
                    x1 = -2f;
                }

                if (x2 > 2f)
                {
                    x2 = 2f;
                }

                Yeni_SpawnPozisyonu.x = Random.Range(x1, x2);

                // Bariyeri oluştur
                GameObject bariyer = Instantiate(
                    Bariyer,
                    Yeni_SpawnPozisyonu,
                    Quaternion.identity
                    );
                bariyer.transform.parent = BariyerList.transform;

            }

        }
    }

    
    








}

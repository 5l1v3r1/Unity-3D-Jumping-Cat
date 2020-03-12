using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OyunYoneticisi : MonoBehaviour {

    // Oyuncu sınıfını çağır
    Oyuncu oyuncu;

    bool isYasiyor;

    // Dalgalar objesi
    public GameObject Dalgalar;

    //// Oyun Sesleri
    //private AudioSource Oyun_AudioSource = null;
    //public AudioClip OyunMuzigi = null;

    /// <summary>
    /// Start metodu
    /// </summary>
    void Start () {
        // Yaşıyorsa
        isYasiyor = true;

        // Oyuncuyu bul
        oyuncu = FindObjectOfType<Oyuncu>();
	}
	

    /// <summary>
    /// Update metodu
    /// </summary>
	void Update () {
        if (isYasiyor)
        {
            DalgalariGetir();
        }
    }

    /// <summary>
    /// Dalga metotu
    /// </summary>
    void DalgalariGetir()
    {
        // Dalgaları yukarıya doğru zaman göre çıkar
        Dalgalar.transform.position += new Vector3(0, (Time.deltaTime * 2f), 0);
    }

    /// <summary>
    /// FixedUpdate metodu
    /// </summary>
    private void FixedUpdate()
    {
        // Eğer oyuncu öldüyse oyunu tekrar başlatır
        if (oyuncu.isOldu)
        {
            // Ölürse
            isYasiyor = false;

            // Burada ölüm menüsü açılacak oyundu dilerse tekrardan başlatacak
            //InvokeRealTime("OyunuTekrarBaslat", 0.5f );
        }
    }


    /// <summary>
    /// Invoke metodunun gelişmişi float değerinde değer verebiliyor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="duration"></param>
    public void InvokeRealTime(string name, float duration)
    {
        Invoke(name, Time.timeScale == 0 ? 0 : duration / Time.timeScale);
    }


    /// <summary>
    /// Oyunu Tekrar başlatma metodu
    /// </summary>
    public void OyunuTekrarBaslat()
    {
        Scene level = SceneManager.GetActiveScene();
        SceneManager.LoadScene(level.name);
    }











}

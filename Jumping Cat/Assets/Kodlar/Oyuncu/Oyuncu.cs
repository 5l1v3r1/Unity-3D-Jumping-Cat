using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Oyuncu : MonoBehaviour {

    // Android Modu
    [Tooltip("Android modunu belirlemenize yarar ona göre kontrolleri değiştirir")]
    public bool isAndroid = false;

    // RigidBody
    Rigidbody2D body2D;

    // Cooliders
    CircleCollider2D circ2D;
    BoxCollider2D box2D;

    // Animasyon kontrol
    Animator OyuncuAnimKontrol;

    /// <summary>
    /// Karakter Özellik Ayarları
    /// </summary>
    [Tooltip("Karakterin ne kadar hızlı gideceğini belirler")]
    [Range (0, 20)]
    public float OyuncuHizi;

    [Tooltip("Karakterin zıplama gücünü belirler")]
    [Range(0, 2000)]
    public float ZiplamaGucu;
    [Tooltip("Karakterin 2. zıplama gücünü belirler")]
    [Range(0, 30)]
    public float DoubleZiplamaGucu;
    internal bool IsDoubleZiplama;
    
    // Yeri bulma
    [Tooltip("Karakter bariyerin üstünde mi kontrol eder")]
    public bool IsBariyer;
    Transform BariyerKontrol;
    const float BariyerKontrolRadius = 0.2f;
    [Tooltip("Bulunduğu yerin ne olduğunu belirler")]
    public LayerMask BariyerLayer;
    

    // Oyuncu Canı
    internal int maxOyuncuCani = 100;
    public int mevcutOyuncuCani;
    internal bool isOyuncuZarar;
    HasarVer hasarVerme;

    // Oyuncuyu öldür
    internal bool isOldu;
    [Tooltip("Oyuncu öldüğünde yukarı doğru bir güç uygular")]
    public float OlduFirlatGucu;

    /// <summary>
    /// Karakter Ses Ayarları
    /// </summary>
    private AudioSource Karakter_AudioSource = null;
    public AudioClip Ziplama_Ses = null;
    public AudioClip Olme_Ses = null;
    public AudioClip Puan_Ses = null;



    /// <summary>
    /// Start Metodu
    /// Oyun Başladığında çalışacak kodlar
    /// </summary>
	void Start () {

        // RigidBody Ayarları
        body2D = GetComponent<Rigidbody2D>();
        body2D.gravityScale = 5;
        body2D.freezeRotation = true;
        body2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Zıplama sesi
        Karakter_AudioSource = GetComponent<AudioSource>();

        // Colliderları al
        circ2D = GetComponent<CircleCollider2D>();
        box2D = GetComponent<BoxCollider2D>();

        // Bariyeri bul
        BariyerKontrol = transform.Find("BariyerKontrol");

        // Oyuncu animasyonu al
        OyuncuAnimKontrol = GetComponent<Animator>();

        // Oyuncunun başlangıç canı
        hasarVerme = FindObjectOfType<HasarVer>();
        mevcutOyuncuCani = maxOyuncuCani;
        
    }


    /// <summary>
    /// Update Metodu
    /// Sürekli çalışacak kodlar
    /// </summary>
    void Update()
    {
        // Metotları kontrol et
        UpdateAnimasyonlar();
        CanAzalt();
        isOldu = mevcutOyuncuCani <= 0;
        OyuncuOldur();

        // Oyuncu canı max candan fazla olmasın!
        if (mevcutOyuncuCani > maxOyuncuCani)
        {
            mevcutOyuncuCani = maxOyuncuCani;
        }
    }


    /// <summary>
    /// FixedUpdate Metodu
    /// Fizik işlemlerinde kullan sadece!
    /// </summary>
	void FixedUpdate () {

        // Bariyer kontrolü
        IsBariyer = Physics2D.OverlapCircle(BariyerKontrol.position, BariyerKontrolRadius, BariyerLayer);

        // Android Modu Aktif ise çalışma
        if(!isAndroid)
        {
            // kontroller
            float h = Input.GetAxis("Horizontal");
            body2D.velocity = new Vector2(h * OyuncuHizi, body2D.velocity.y);

            // Karakter sağa, sola bakma
            KarakterCevir(h);
        }

	}


    /// <summary>
    /// Android için sağa ve sola hareket etme metodu
    /// </summary>
    /// <param name="sag"></param>
    public void Hareket(bool sag)
    {
        if (sag)
        {
            body2D.velocity = new Vector2(OyuncuHizi, body2D.velocity.y);
            KarakterCevir(1);
        }
        else
        {
            body2D.velocity = new Vector2(-OyuncuHizi, body2D.velocity.y);
            KarakterCevir(-1);
        }
    }


    /// <summary>
    /// Hareketi sıfırlama metodu
    /// </summary>
    public void VolocitySifirla()
    {
        body2D.velocity = Vector2.zero;
    }


    /// <summary>
    /// Zıplama metodu
    /// </summary>
    public void Ziplama()
    {
        if (IsBariyer)
        {
            // Rigidbody'ye dikey yönde (Y) güç uygular
            body2D.AddForce(new Vector2(0, ZiplamaGucu));
            IsDoubleZiplama = true;
            
            // Zıplama sesi bir kez çal
            Karakter_AudioSource.PlayOneShot(Ziplama_Ses);
        }
        else
        {
            DoubleZiplama();
        }
    }


    /// <summary>
    /// Double Zıplama metodu
    /// </summary>
    public void DoubleZiplama()
    {
        if (!IsBariyer && IsDoubleZiplama)
        {
            // Rigidbody'ye dikey yönde (Y) ani güç uygular
            body2D.AddForce(new Vector2(0, DoubleZiplamaGucu), ForceMode2D.Impulse);
            IsDoubleZiplama = false;
        }
    }
    

    /// <summary>
    /// Karakterin sağa ve sola bakması
    /// </summary>
    /// <param name="h"></param>
    private void KarakterCevir(float h)
    {
        // Sağa bakış
        if(h > 0)
        {
            // Karakter Collider değerleri
            GetComponent<SpriteRenderer>().flipX = false;

            // Circle2D
            Vector2 deger = new Vector2(-0.31f, 0.59f);
            circ2D.offset = deger;
            // Box2D
            Vector2 deger2 = new Vector2(-0.15f, -1.149949f);
            box2D.offset = deger2;

        }

        // Sola bakış
        if (h < 0)
        {
            // Karakter Collider değerleri
            GetComponent<SpriteRenderer>().flipX = true;

            // Circle2D
            Vector2 deger = new Vector2(-0.25f, 0.59f);
            circ2D.offset = deger;

            // Box2D
            Vector2 deger2 = new Vector2(-0.14f, -1.33999f);
            box2D.offset = deger2;
        }
    }


    /// <summary>
    /// Animasyonları güncelleme metodu
    /// </summary>
    void UpdateAnimasyonlar()
    {
        OyuncuAnimKontrol.SetFloat("VelocityX", Mathf.Abs(body2D.velocity.x));
        OyuncuAnimKontrol.SetBool("isBariyer", IsBariyer);
        OyuncuAnimKontrol.SetFloat("VelocityY", body2D.velocity.y);
        OyuncuAnimKontrol.SetBool("isOldu", isOldu);
        if (isOyuncuZarar && !isOldu)
        {
            OyuncuAnimKontrol.SetTrigger("isZarar");
        }

    }

    /// <summary>
    /// Oyuncunun canını azaltma metodu
    /// </summary>
    void CanAzalt()
    {
        if (isOyuncuZarar)
        {
            // Oyuncunun canından gelen zarar kadar çıkar
            mevcutOyuncuCani -= hasarVerme.Hasar;
            
            // Ölme sesi bir kez çal
            Karakter_AudioSource.PlayOneShot(Olme_Ses);

            isOyuncuZarar = false;
        }
    }

    /// <summary>
    /// Oyuncu öldürme metodu
    /// </summary>
    void OyuncuOldur()
    {
        if (isOldu)
        {
            // Oyuncu zarar false çevir zarar alamasın artık
            isOyuncuZarar = false;

            // Oyuncuya yukarı doğru bir ivme ver ve düşür
            body2D.AddForce(new Vector2(0, OlduFirlatGucu), ForceMode2D.Impulse);
            body2D.drag += Time.deltaTime * 90;
            OlduFirlatGucu -= Time.deltaTime * 20;
            body2D.constraints = RigidbodyConstraints2D.FreezePositionX;

            // Colliderları false çevir her yerden geçsin
            circ2D.enabled = false;
            box2D.enabled = false;
        }
    }


    /// <summary>
    /// OnTriggerExit2D metodu
    /// </summary>
    /// <param name="obje"></param>
    private void OnTriggerExit2D(Collider2D obje)
    {
        // Bariyerin üstünden geçerse etkileşim kur
        if (obje.gameObject.tag == "Bariyer")
        {
            Karakter_AudioSource.PlayOneShot(Puan_Ses);
            obje.gameObject.GetComponent<BariyerColliderKontrol>().isBoxTrigger = true;
        }
    }

    /// <summary>
    /// OnTriggerEnter2D metodu
    /// </summary>
    /// <param name="obje"></param>
    private void OnTriggerEnter2D(Collider2D obje)
    {
        // Bariyerin üstünden geçerse bariyeri kapat
        if (obje.gameObject.tag == "Bariyer_Trigger")
        {
            obje.gameObject.GetComponent<BariyerColliderKontrol>().isBoxTrigger_Ust = true;
        }
    }


}

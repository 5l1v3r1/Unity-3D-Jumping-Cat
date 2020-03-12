using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BariyerColliderKontrol : MonoBehaviour {

    BoxCollider2D box2D;
    BoxCollider2D box2D_Ust;
    internal bool isBoxTrigger;
    internal bool isBoxTrigger_Ust;
    

	void Start () {
        box2D = GetComponent<BoxCollider2D>();
        box2D_Ust = transform.parent.GetComponent<BoxCollider2D>();
    }
	
	void Update () {

        if (isBoxTrigger)
        {
            box2D.isTrigger = false;
        }

        if (isBoxTrigger_Ust)
        {
            InvokeRealTime("BariyerKapat", 0.3f);
        }
    }

    void BariyerKapat()
    {
        box2D_Ust.isTrigger = false;
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
}

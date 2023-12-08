using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfGoodsOnShelf : MonoBehaviour
{
    //貨架上專用
    public static bool isGoodsOnMe_sf = false;
    public bool isGoodsOnMe_ins = false;
    public static bool ThisisGoodsShelf = false;

    public string ColliderName;
    int count = 0;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        isGoodsOnMe_sf = false;
        isGoodsOnMe_ins = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goods")
        {
            isGoodsOnMe_sf = true;
            isGoodsOnMe_ins = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Goods")
        {
            isGoodsOnMe_sf = false;
            isGoodsOnMe_ins = false;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfGoodsOnMe : MonoBehaviour
{
    //堆高機貨叉上專用
    public static bool isGoodsOnMe_fk = false;
    public bool isGoodsOnMe_ins = false;
    public static bool ThisisGoodsShelf = false;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        isGoodsOnMe_fk = false;
        isGoodsOnMe_ins = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goods")
        {
            isGoodsOnMe_fk = true;
            isGoodsOnMe_ins = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Goods")
        {
            isGoodsOnMe_fk = false;
            isGoodsOnMe_ins  = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class IfAlmostTouch : MonoBehaviour
{
    public static bool isForkliftAlmostTouch = false;
    public  bool isForkliftAlmostTouch_B = false; //為了在inspector裡面看

    public static bool isGoodsAlmostTouch = false;
    public  bool isGoodsAlmostTouch_B = false; //為了在inspector裡面看

    void Start()
    {
        Init();
    }

    public void Init()
    {
        isForkliftAlmostTouch = false;
        isForkliftAlmostTouch_B = false;

        isGoodsAlmostTouch = false;
        isGoodsAlmostTouch_B = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Forkleft")
        {
            isForkliftAlmostTouch = true;
            isForkliftAlmostTouch_B = true;
        }
        else if (other.gameObject.tag == "Goods") {
            isGoodsAlmostTouch = true;
            isGoodsAlmostTouch_B = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Forkleft")
        {
            isForkliftAlmostTouch = false;
            isForkliftAlmostTouch_B = false;
        }
        else if (other.gameObject.tag == "Goods")
        {
            isGoodsAlmostTouch = false;
            isGoodsAlmostTouch_B = false;

        }
    }
}

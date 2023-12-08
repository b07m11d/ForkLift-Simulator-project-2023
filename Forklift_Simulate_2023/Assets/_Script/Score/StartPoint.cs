using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public bool isOnStartPoint_Forkit;
    public static bool isOnStartPoint_Forkift;  //by lin 2023-11-20
    public bool isNeedToBackStartPoint;



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Forkleft")
        {
            isOnStartPoint_Forkit = true;
            isOnStartPoint_Forkift = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Forkleft")
        {
            isOnStartPoint_Forkit = true;
            isOnStartPoint_Forkift = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Forkleft")
        {
            isOnStartPoint_Forkit = false;
            isOnStartPoint_Forkift = false;
        }
    }

}

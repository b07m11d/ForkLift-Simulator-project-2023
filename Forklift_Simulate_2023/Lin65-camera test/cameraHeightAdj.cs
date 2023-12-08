using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHeightAdj : MonoBehaviour
{
    //private GameObject camera;
    private bool isRotating, F7press, F8press;



    void Update()
    {
        // 鍵盤事件，切換視角
        if (Input.GetKeyDown(KeyCode.F7))
        {
            //isRotating = true;
            //F7press = true;
            transform.Translate(0, Time.deltaTime * 5, 0);
        }
        /*else if (Input.GetKeyUp(KeyCode.F7))
        {
            isRotating = false;  // 回到原位
            F7press = false;
        }*/

        if (Input.GetKeyDown(KeyCode.F8))
        {

            //isRotating = true;
            //F8press = true;

            transform.Translate(0, Time.deltaTime * -5, 0);
        }
        /*else if (Input.GetKeyUp(KeyCode.F8))
        {
            isRotating = false;  // 回到原位
            F8press = false;
        }*/



        /*if (isRotating)
        {

            if (F7press)
            {
                transform.Translate(0, Time.deltaTime * 5, 0);
            }
            else if (F8press)
            {
                transform.Translate(0, Time.deltaTime * -5, 0);
            }
        }
        /*else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * 5);*/


    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraViewSwitch : MonoBehaviour
{
    public Transform targetF5,targetF6,camF5,camF6;
    private bool isRotating, F5press, F6press;
       
    private Quaternion originalRotation;
    private Vector3 originalPosition;
    public float speed = 0.1f;  // 轉向的速度

    void Start()
    {
        originalRotation = transform.rotation;  // 保存原始角度
        originalPosition = transform.position;  // 保存原始位置

    }

    void Update()
    {
        // 鍵盤事件，切換視角
        if (Input.GetKeyDown(KeyCode.F5))
        {
            //transform.LookAt(targetF5);
            isRotating = true;
            F5press = true;
        }
        else if (Input.GetKeyUp(KeyCode.F5))
        {
            isRotating = false;  // 回到原位
            F5press = false;
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {

            isRotating = true;
            F6press = true;
        }
        else if (Input.GetKeyUp(KeyCode.F6))
        {
            isRotating = false;  // 回到原位
            F6press = false;
        }



        if (isRotating)
        {

            if (F5press)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetF5.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
                transform.position = Vector3.Lerp(transform.position, camF5.position, speed);
            }
            else if (F6press)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetF6.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
                transform.position = Vector3.Lerp(transform.position, camF6.position, speed);
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * 5);
            transform.position = Vector3.Lerp(transform.position, originalPosition, speed);
        }
        
    }
    
}

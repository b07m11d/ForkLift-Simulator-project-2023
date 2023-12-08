using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeForkleftView : MonoBehaviour
{
    [SerializeField]
    GameObject FrontCamera;
    [SerializeField]
    GameObject BackCamera;
    [SerializeField]
    GameObject RightCamera;
    [SerializeField]
    GameObject LeftCamera;
    [SerializeField]
    GameObject TopCameraImage;
    [SerializeField]
    GameObject ForkCameraImage;
    [SerializeField]
    GameObject ForkCamera;


    LogtichControl logtichControl;

    private bool flag = false;


    void Start()
    {
        FrontCamera.SetActive(true);
        BackCamera.SetActive(false);
        RightCamera.SetActive(false);
        LeftCamera.SetActive(false);
        logtichControl = GetComponentInParent<LogtichControl>();

        ForkCameraImage.SetActive(false);
        ForkCamera.SetActive(false);
        flag = false;

        TopCameraImage.SetActive(false);
    }

    void Update()
    {
        if (logtichControl.CameraFront)// || GetComponent<WSMGameStudio.Vehicles.WSMVehicleController>().BackFrontInput == 1
        {
            FrontCamera.SetActive(true);
            BackCamera.SetActive(false);
            RightCamera.SetActive(false);
            LeftCamera.SetActive(false);
        }
        if (logtichControl.CameraBack)// || GetComponent<WSMGameStudio.Vehicles.WSMVehicleController>().BackFrontInput == -1
        {
            FrontCamera.SetActive(false);
            BackCamera.SetActive(true);
            RightCamera.SetActive(false);
            LeftCamera.SetActive(false);
        }
        if (logtichControl.CameraRight)
        {
            FrontCamera.SetActive(false);
            BackCamera.SetActive(false);
            RightCamera.SetActive(true);
            LeftCamera.SetActive(false);
        }
        if (logtichControl.CameraLeft)
        {
            FrontCamera.SetActive(false);
            BackCamera.SetActive(false);
            RightCamera.SetActive(false);
            LeftCamera.SetActive(true);
        }


        /*if (CurrentPosLimit.isInPosLimit)
        {
            TopCameraImage.SetActive(true);
        }
        else
        {
            TopCameraImage.SetActive(false);

        }*/

        if (Input.GetKeyDown(KeyCode.F4)) //|| Input.GetKey(KeyCode.JoystickButton9)
        {
            if (flag == false)  //按第一次
            {
                //按下鍵盤按鍵之後，顯示貨插畫面並且flag=true;
                //Debug.Log("Flag=1");
                flag = true;
                ForkCamera.SetActive(true);
                ForkCameraImage.SetActive(true);
            }
            else if (flag == true)  //在按一次
            {
                //按下鍵盤按鍵之後，隱藏貨插畫面並且flag=false;
                //Debug.Log("Flag=0");
               
                if (TopCameraImage.active == true) {
                    TopCameraImage.SetActive(false);
                }
                flag = false;
                ForkCamera.SetActive(false);
                ForkCameraImage.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.F3)) //|| Input.GetKey(KeyCode.JoystickButton9)
        {
            if (flag == false  )
            {
                //按下鍵盤按鍵之後，顯示貨插畫面並且flag=true;
                //Debug.Log("Flag=1");
                flag = true;
                TopCameraImage.SetActive(true);
            }
            else if (flag == true )
            {
                //按下鍵盤按鍵之後，隱藏貨插畫面並且flag=false;
                //Debug.Log("Flag=0");

                if (ForkCamera.active == true)
                {
                    ForkCamera.SetActive(false);
                    ForkCameraImage.SetActive(false);
                }
                
                flag = false;
                TopCameraImage.SetActive(false);
            }
        }


    }
}

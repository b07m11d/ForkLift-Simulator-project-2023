using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetAllJoysEvent : MonoBehaviour
{
    #region Camera movements 我想弄出相機移動
    public bool isRotating, F4press, F5press, F6press, F7press, F8press, Bkspress;
    public Transform targetF5t, targetF6t, camF5t, camF6t;
    public GameObject targetF5, targetF6, camF5, camF6, MsgBox, SteeringWh, LookAtFoot;

    //PC版切換相機視角用的模式判斷
    public int CamMode = 1;

    //原始相機位置
    public Vector3 originalCamPos;

    public GameObject a, b, c, d;  //運算用targetF55, targetF66, camF55, camF66

    private Quaternion originalRotation, originalRotationd;
    private Vector3 originalPosition, originalPositiond;

    public float speed = 2.5f;  // 轉向的速度
    public static float pot1, pot2;   //Lin +
    #endregion

    [HideInInspector]
    public static bool HandBrake_btn4;
    [HideInInspector]
    public static bool FrontBar_btn5;
    [HideInInspector]
    public static bool FrontBar_btn6;
    [HideInInspector]
    public static float UpDownBar_Ry_升降;
    [HideInInspector]
    public static float DegreeBar_Rz_傾斜;

    private string currentButton, currentButton1;

    void Awake()
    {

    }

    void Start()
    {
        Debug.Log("相機移動");
        #region Camera movements 我(Lin Liang Yu)弄出了相機移動
        isRotating = false;

        //c = GameObject.Find("forkliftCustom04_ChainOk_VR(Clone)/[VRTK_SDKManager]/[VRTK_SDKSetups]");//c

        //c = GameObject.Find("forkliftCustom04_ChainOk_VR(Clone)/[VRTK_SDKManager]/[VRTK_SDKSetups]/SteamVR");//c
        //d = GameObject.Find("forkliftCustom04_ChainOk_VR(Clone)/[VRTK_SDKManager]/[VRTK_SDKSetups]/SteamVR/[CameraRig]");//d
        //a = GameObject.Find("forkliftCustom04_ChainOk_VR(Clone)/[VRTK_SDKManager]/[VRTK_SDKSetups]/VRSimulator/[VRSimulator_CameraRig]"); //a
        //b = GameObject.Find("forkliftCustom04_ChainOk_VR(Clone)/[VRTK_SDKManager]/[VRTK_SDKSetups]/VRSimulator/[VRSimulator_CameraRig]/Neck");//b


        //VR mode
        /*
         * c = GameObject.Find("forkliftCustom04_ChainOk_VR(Clone)/[VRTK_SDKManager]/[VRTK_SDKSetups]/VRSimulator/");//c
        d = GameObject.Find("forkliftCustom04_ChainOk_VR(Clone)/[VRTK_SDKManager]/[VRTK_SDKSetups]/VRSimulator/[VRSimulator_CameraRig]");//d
        a = GameObject.Find("forkliftCustom04_ChainOk_VR(Clone)/[VRTK_SDKManager]/[VRTK_SDKSetups]"); //a
        b = GameObject.Find("forkliftCustom04_ChainOk_VR(Clone)/[VRTK_SDKManager]/[VRTK_SDKSetups]/VRSimulator/[VRSimulator_CameraRig]/Neck");//b
        */

        //PC mode
        b = GameObject.Find("forkliftCustom04_ChainOk(Clone)/FirstPersonCam");
        c = GameObject.Find("forkliftCustom04_ChainOk(Clone)/FirstPersonCam/Camera");//c


        //c.SetActive(true);

        //d.transform.position = new Vector3(7.3f, -.6f, -6.0f); //VR
        //c.transform.localPosition = new Vector3(-0.06f, .26f, -.35f); //PC 起始位置


        //a = GetComponent<VRSimulatorCameraRig>();

        originalRotation = c.transform.rotation;  // 保存原始角度
        originalPosition = c.transform.localPosition;  // 保存原始位置

        originalRotationd = c.transform.rotation;  // 保存原始角度
        originalPositiond = c.transform.localPosition;  // 保存原始位置

        //Debug.Log(originalPositiond);

        //創新物件，2023年6月改成直接放在prefab 裡面
        /*GameObject targetF55 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        targetF55.name = "targetF5";  //Name shows in Hierarchy
        GameObject camF55 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        camF55.name = "camF5";
        GameObject targetF66 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        targetF66.name = "targetF6";
        GameObject camF66 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        camF66.name = "camF6";

        //GameObject.find
        //讓null的t變項=T.transform
        targetF5 = GameObject.Find("ForkitOriPos_start/forkliftCustom04_ChainOk(Clone)/FirstPersonCam/targetF5");
        targetF5t = targetF5.transform;
        camF5 = GameObject.Find("ForkitOriPos_start/forkliftCustom04_ChainOk(Clone)/FirstPersonCam/camF5");
        camF5t = camF5.transform;
        targetF6 = GameObject.Find("ForkitOriPos_start/forkliftCustom04_ChainOk(Clone)/FirstPersonCam/targetF6");
        targetF6t = targetF6.transform;
        camF6 = GameObject.Find("ForkitOriPos_start/forkliftCustom04_ChainOk(Clone)/FirstPersonCam/camF6");
        camF6t = camF6.transform;*/

        //讓target cube 和 camera cube(用來記position 用的) 各自變成相對的
        //2023年6月改成直接放在prefab 裡面
        /*targetF5.transform.position = b.transform.position;
        targetF5.transform.localScale = new Vector3(.1f, .1f, .1f);
        targetF5.transform.SetParent(c.transform);
        targetF5.GetComponent<BoxCollider>().enabled = false;
        targetF5.transform.position = new Vector3(6.4f, 0.5f, -5.4f);    //new Vector3(0, 0.5f, 0);
        targetF5.GetComponent<MeshRenderer>().enabled = false;
        camF5.transform.position = b.transform.position;
        camF5.transform.localScale = new Vector3(.1f, .1f, .1f);
        camF5.transform.SetParent(c.transform);
        camF5.GetComponent<BoxCollider>().enabled = false;
        camF5.transform.position = new Vector3(6.7f, 1.5f, -5.6f);
        camF5.GetComponent<MeshRenderer>().enabled = false;

        targetF6.transform.position = b.transform.position;
        targetF6.transform.localScale = new Vector3(.1f, .1f, .1f);
        targetF6.transform.SetParent(c.transform);
        targetF6.GetComponent<BoxCollider>().enabled = false;
        targetF6.transform.position = new Vector3(8.4f, 0.5f, -5.4f);    //new Vector3(0, 0.5f, 0);
        targetF6.GetComponent<MeshRenderer>().enabled = false;
        camF6.transform.position = b.transform.position;
        camF6.transform.localScale = new Vector3(.1f, .1f, .1f);
        camF6.transform.SetParent(c.transform);
        camF6.GetComponent<BoxCollider>().enabled = false;
        camF6.transform.position = new Vector3(8.1f, 1.5f, -5.6f);
        camF6.GetComponent<MeshRenderer>().enabled = false;   
        */
        #endregion

        Vector3 originalCamPos = c.transform.localPosition; // 紀錄
    }

    void Update()
    {
        //   this line also

        var values = Enum.GetValues(typeof(KeyCode));
        //var spanAxis = Enum.GetValues(typeof(SnapAxis));



        //存储所有的按键 
        for (int x = 0; x < values.Length; x++)
        {
            if (Input.GetKeyDown((KeyCode)values.GetValue(x)))
            {
                currentButton = values.GetValue(x).ToString();
                //Debug.Log("Current Button : " + currentButton);

            }
        }




        //手煞、前進、退後
        //2023-07-26 這三個功能是用在arduino 上，已經導致羅技方向盤同時被占用一些button...
        HandBrake_btn4 = false;
        FrontBar_btn5 = false;
        FrontBar_btn6 = false;

        if (Input.GetKey(KeyCode.JoystickButton4))//從Button0開始算
        {
            HandBrake_btn4 = true;
            //Debug.Log("按下按钮4_手煞");
        }

        if (Input.GetKey(KeyCode.JoystickButton5))//從Button0開始算
        {
            FrontBar_btn5 = true;
            //Debug.Log("按下按钮5_前進");
        }


        if (Input.GetKey(KeyCode.JoystickButton6))//從Button0開始算
        {
            FrontBar_btn6 = true;
            //Debug.Log("按下按钮6_後退");
        }

        //升降、傾斜拉桿
        // UpDownBar_Ry_升降 = Input.GetAxis("Horizontal");
        // DegreeBar_Rz_傾斜 = Input.GetAxis("Handbar_outside");
        UpDownBar_Ry_升降 = ArduinoPort.instance.pot1;
        DegreeBar_Rz_傾斜 = ArduinoPort.instance.pot2;



        //Debug.Log(ArduinoPort.instance.pot1);
        //UpDownBar_Ry_升降 = Arduino     .instance.pot1;
        //Debug.Log("Horizontal:" + Input.GetAxis("Horizontal"));//方向盤
        //Debug.Log("Vertical:" + Input.GetAxis("Vertical"));//吋動踏板
        //Debug.Log("Handbar_inside:" + Input.GetAxis("Handbar_inside"));//升降
        //Debug.Log("Handbar_outside:" + Input.GetAxis("Handbar_outside"));//傾斜


        //   Lin + @2023-02-05

        /*        pot1 = Input.GetAxis("Handbar_inside");              //   Lin +
                pot2 = Input.GetAxis("Handbar_outside");*/

        pot1 = ArduinoPort.instance.pot1;              //   Lin +
        pot2 = ArduinoPort.instance.pot2;

        #region Camera movements 我想弄出相機移動
        if (Input.GetKeyDown(KeyCode.F7)) //高度+
        {
            b.transform.Translate(0, Time.deltaTime * 1.5f, 0);
        }
        /*if (Input.GetKey(KeyCode.JoystickButton7))//從Button0開始算
        {
            c.transform.Translate(0, Time.deltaTime * 1, 0);
        }*/


        if (Input.GetKeyDown(KeyCode.F8)) //高度-
        {
            b.transform.Translate(0, Time.deltaTime * -1.5f, 0);
        }
        /*if (Input.GetKey(KeyCode.JoystickButton6))//從Button0開始算
        {
            c.transform.Translate(0, Time.deltaTime * -1, 0);
        }*/


        if (Input.GetKeyDown(KeyCode.F9)) //水平往左
        {

            b.transform.Translate(Time.deltaTime * -1, 0, 0);
        }
        /*if (Input.GetKey(KeyCode.JoystickButton1))//從Button0開始算
        {
            c.transform.Translate(Time.deltaTime * -1, 0, 0);
        }*/


        if (Input.GetKeyDown(KeyCode.F10))//水平往右
        {
            b.transform.Translate(Time.deltaTime * 1, 0, 0);
        }
        /*if (Input.GetKey(KeyCode.JoystickButton2))//從Button0開始算
        {
            c.transform.Translate(Time.deltaTime * 1, 0, 0);
        }*/


        if (Input.GetKeyDown(KeyCode.F11)) //水平往前
        {

            b.transform.Translate(0, 0, Time.deltaTime * 1.5f);
        }
        /*if (Input.GetKey(KeyCode.JoystickButton3))//從Button0開始算
        {
            c.transform.Translate(0, 0, Time.deltaTime * 1);
        }*/


        if (Input.GetKeyDown(KeyCode.F12))//水平往後
        {
            b.transform.Translate(0, 0, Time.deltaTime * -1.5f);
        }
        /*if (Input.GetKey(KeyCode.JoystickButton0))//從Button0開始算
        {
            c.transform.Translate(0, 0, Time.deltaTime * -1);
        }*/



        // 鍵盤事件，切換視角
        if (Input.GetKey(KeyCode.F5)) //|| Input.GetKey(KeyCode.JoystickButton11)
        {
            isRotating = true;
            F5press = true;
            //Debug.Log("F5 pressdown.");
        }
        else if (Input.GetKeyUp(KeyCode.F5)) //|| Input.GetKey(KeyCode.JoystickButton11)
        {
            isRotating = false;  // 回到原位
            F5press = false;
        }


        if (Input.GetKey(KeyCode.F6)) //|| Input.GetKey(KeyCode.JoystickButton10)
        {

            isRotating = true;
            F6press = true;
        }
        else if (Input.GetKeyUp(KeyCode.F6)) //|| Input.GetKey(KeyCode.JoystickButton10)
        {
            isRotating = false;  // 回到原位
            F6press = false;
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) //|| Input.GetKey(KeyCode.JoystickButton10)
        {
            /*isRotating = true;
            Bkspress = true;*/
            CamMode += 1;

            //相機要三段式切換

            if (CamMode == 1)
            {
                b.transform.localRotation = Quaternion.LookRotation(SteeringWh.transform.localPosition);
            }
            else if (CamMode == 2)
            {
                b.transform.localRotation = Quaternion.LookRotation(LookAtFoot.transform.localPosition);
            }
            else if (CamMode == 3)
            {
                b.transform.localRotation = Quaternion.LookRotation(MsgBox.transform.localPosition);
            }
            else if (CamMode > 3) {
                b.transform.localRotation = Quaternion.LookRotation(SteeringWh.transform.localPosition);
                CamMode = 1;
            }
        }

        /*
        switch (CamMode)
        {
            case 1: // 模式1

            case 2: // 模式2，第一次按，看下面


                break;
            case 3: // 模式3，第二次按，看訊息欄

                c.transform.rotation = Quaternion.LookRotation(MsgBox.transform.localPosition);
                break;
            // 此為預設 當上面的case都沒達成時則會判斷
            default:
                CamMode = 1;
                break;
        }*/

        /*else if (Input.GetKeyUp(KeyCode.Backspace)) //|| Input.GetKey(KeyCode.JoystickButton10)
        {
            /*isRotating = false;  // 回到原位
            Bkspress = false;
            c.transform.rotation = Quaternion.LookRotation(originalCamPos);
        }*/



        /*if (Input.GetKey(KeyCode.F4))
        {

            isRotating = true;
            F4press = true;
        }
        else if (Input.GetKeyUp(KeyCode.F4))
        {
            isRotating = false;  // 回到原位
            F4press = false;
        }*/



        if (isRotating == true)
        {
            

            if (F5press)
            {
                c.transform.position = Vector3.Lerp(c.transform.position, camF5.transform.position, Time.deltaTime * speed);
            }
            else if (F6press)
            {
                c.transform.position = Vector3.Lerp(c.transform.position, camF6.transform.position, Time.deltaTime * speed);
            }
            /*else if (Bkspress)
            {
                //c.transform.position = Vector3.Lerp(c.transform.position, MsgBox.transform.position, Time.deltaTime * speed);
                Vector3 dir = MsgBox.transform.localPosition - c.transform.localPosition;
                c.transform.rotation = Quaternion.LookRotation(dir);

            }*/

        }
        else if (isRotating == false)
        {
            c.transform.localPosition = Vector3.Lerp(c.transform.localPosition, originalPosition, Time.deltaTime * speed);
            
        }


        /*if (Bkspress)
        {
            //c.transform.position = Vector3.Lerp(c.transform.position, MsgBox.transform.position, Time.deltaTime * speed);
            Quaternion targetRotation = Quaternion.LookRotation(MsgBox.transform.localPosition - c.transform.localPosition);

        }
        else if (!Bkspress)
        {
        
        }*/


            /*if (F5press && isRotating== true)
                {
                //Quaternion targetRotation = Quaternion.LookRotation(camF5t.localPosition - d.transform.position);
                //Quaternion targetRotation = Quaternion.LookRotation(camF5t.localPosition - c.transform.localPosition);
                //d.transform.rotation = Quaternion.Slerp(d.transform.rotation, targetRotation, Time.deltaTime * speed);
                //d.transform.localPosition = Vector3.Lerp(d.transform.localPosition, targetF5t.localPosition, speed);


                //c.transform.position = Vector3.Lerp(c.transform.position, camF5t.position, speed);
                c.transform.rotation = Quaternion.Lerp(c.transform.rotation, camF5t.rotation, speed);


            }
                else if (F6press && isRotating== true)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(camF6t.localPosition - d.transform.position);
                    d.transform.rotation = Quaternion.Slerp(d.transform.rotation, camF6t.rotation, Time.deltaTime * speed);
                    //d.transform.localPosition = Vector3.Lerp(d.transform.position, targetF6t.position, speed);
                }
                /*else if (F4press && isRotating== true)
                {
                    d.transform.rotation = Quaternion.Slerp(d.transform.rotation, originalRotation, Time.deltaTime * speed);
                    d.transform.position = Vector3.Lerp(d.transform.position, originalPosition, speed);
                }
                else
            {
                //c.transform.rotation = Quaternion.Slerp(c.transform.rotation, originalRotation, Time.deltaTime * speed);
                //c.transform.position = Vector3.Lerp(c.transform.position, originalPositiond, speed);

                d.transform.rotation = Quaternion.Slerp(d.transform.rotation, originalRotation, Time.deltaTime * speed);
                d.transform.localPosition = Vector3.Lerp(d.transform.localPosition, originalPosition, speed);
            }*/

            #endregion

        }
}

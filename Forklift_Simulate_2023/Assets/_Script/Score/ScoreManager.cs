﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    MainGameManager mainGameManager;
    StageThreeGameManager mainGameManager_stageThree;


    [HideInInspector]
    public int TotalWrongAmount = 0; //以5為一單位

    [HideInInspector]
    public float Timer;


    [HideInInspector]
    public int TimeScore;
    [HideInInspector]
    public int pipeFallScore;
    [HideInInspector]
    public int ForkitOnLineScore;
    [HideInInspector]
    public int StopTooLongScore;
    [HideInInspector]
    public int SpeedToHightScore;
    [HideInInspector]
    public int ForkPositionHightScore;
    [HideInInspector]
    public int ForkMastTiltScore;
    [HideInInspector]
    public int ForkHandBrakeScore;
    [HideInInspector]
    public int ForkCluthScore;
    [HideInInspector]
    public int ForkBackFrontNorStopScore;
    [HideInInspector]
    public int OnRoadNotEngineScore;
    //=====================第三關=====================
    [HideInInspector]
    public int ForkitTouchShelfScore;
    [HideInInspector]
    public int ForkitToFarto後扶架Score;
    [HideInInspector]
    public int GoodTouchGroundLineScore;
    [HideInInspector]
    public int GoodTouchFloorScore;
    //========第三關====================2023-11-08 action by lin liang yu
    public int GoodsOnShelfOK_C;
    public int ForkInsertGoods_C;
    public int AlmostTouch_C;
    public int GoodsAlmostTouch_C;// by lin @　2023-11-17；_C 表示 count 計次數


    public UnityAction<int> OnTimeScore;
    public UnityAction<int> OnPipeFallScore;
    public UnityAction<int> OnForkitOnLineScore;
    public UnityAction<int> OnStopTooLongScore;
    public UnityAction<int> OnSpeedToHightScore;
    public UnityAction<int> OnForkPositionHightScore;
    public UnityAction<int> OnForkMastTiltScore;
    public UnityAction<int> OnForkHandBrakeScore;
    public UnityAction<int> OnForkCluthScore;
    public UnityAction<int> OnForkBackFrontNorStopScore;
    public UnityAction<int> OnOnRoadNotEngineScore;
    //=====================第三關=====================
    public UnityAction<int> OnForkiTouchShelfScore;
    public UnityAction<int> OnForkitToFarTo後扶架Score;
    public UnityAction<int> OnGoodTouchGroundLineScore;
    public UnityAction<int> OnGoodTouchFloorScore;
    //========第三關====================2023-11-08 action by lin liang yu
    public UnityAction<int> OnGoodsOnShelfOK;
    public UnityAction<int> OnForkInsertGoods;
    public UnityAction<int> OnAlmostTouch;
    public UnityAction<int> OnGoodsAlmostTouch;// by lin @　2023-11-17





    [SerializeField]
    AudioClip[] wrongVoice;

    AudioSource AS;

    PipeDetectControl _pipeDetectControl;
    WSMGameStudio.Vehicles.WSMVehicleController _wSMVehicleController;
    WSMGameStudio.HeavyMachinery.ForkliftController _forkliftController;

    bool isTimeUp = false;
    bool isCheckOnRoadLine = false;
    bool isCheckStopTooLong = false;
    bool isCheckToHightSpeed = false;
    bool isCheckPositionHight = false;
    bool isChecMastTilt = false;
    bool isCheckForkHandBrake = false;
    //bool isCheckBrake = false;
    bool isCheckForkCluth = false;
    bool isCheckBackFront = false;
    bool isCheckOnRoadNotEngine = false;
    //===========第三關===================
    bool isCheckTouchShelf = false;
    bool isCheckToFarTo後扶架 = false;
    bool isCheckGoodTouchGroundLine = false;
    bool isCheckGoodTouchFloor = false;
    //lin liang yu add @ 2023-10-25
    bool isForkliftAlmostTouch = false;  
    bool isForkInsertGoods = false;
    bool isGoodsOnShelfOK = false;
    bool isGoodsAlmostTouch = false;  // by lin @　2023-11-17

    //用來在inspector 看數字用的
    [SerializeField]
    public float CurrentMastTilt_ins;
    public float CurrentPositionHight_ins;


    public float MastTilt_ins = 0.57f;
    public float PositionHight_ins = 0.052f;

    public int wrongAmount_ins;



    LogtichControl logtichControl;
    bool startTime = false;
    //private void OnGUI()
    //{
    //    GUI.color = Color.black;
    //    GUI.Box(new Rect(10, 10, 210, 250),"");
    //    GUI.color = Color.white;
    //    GUI.Label(new Rect(10, 10, 200, 100), "pipeFallScore"+ pipeFallScore);
    //    GUI.Label(new Rect(10, 30, 200, 100), "SpeedToHightScore" + SpeedToHightScore);
    //    GUI.Label(new Rect(10, 50, 200, 100), "ForkPositionHightScore" + ForkPositionHightScore);
    //    GUI.Label(new Rect(10, 70, 200, 100), "ForkMastTiltScore" + ForkMastTiltScore);
    //    GUI.Label(new Rect(10, 90, 200, 100), "ForkHandBrakeScore" + ForkHandBrakeScore);
    //    //GUI.Label(new Rect(10, 110, 200, 100), "ForkBrakeScore" + ForkBrakeScore);
    //    GUI.Label(new Rect(10, 110, 200, 100), "ForkCluthScore" + ForkCluthScore);
    //    GUI.Label(new Rect(10, 130, 200, 100), "ForkBackFrontNorStopScore" + ForkBackFrontNorStopScore);
    //    GUI.Label(new Rect(10, 150, 200, 100), "OnRoadNotEngineScore" + OnRoadNotEngineScore);
    //}

    //void Start()
    //{
    //    Init();
    //}

    public void Init()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainGameState":
                mainGameManager = this.GetComponent<MainGameManager>();
                _pipeDetectControl = mainGameManager.PipeGroupObjs.GetComponent<PipeDetectControl>();
                _wSMVehicleController = mainGameManager.ForkleftObj.GetComponent<WSMGameStudio.Vehicles.WSMVehicleController>();
                _forkliftController = mainGameManager.ForkleftObj.GetComponent<WSMGameStudio.HeavyMachinery.ForkliftController>();

                break;
            case "ThridStage":

                mainGameManager_stageThree = this.GetComponent<StageThreeGameManager>();
                _pipeDetectControl = mainGameManager_stageThree.PipeGroupObjs.GetComponent<PipeDetectControl>();
                _wSMVehicleController = mainGameManager_stageThree.ForkleftObj.GetComponent<WSMGameStudio.Vehicles.WSMVehicleController>();
                _forkliftController = mainGameManager_stageThree.ForkleftObj.GetComponent<WSMGameStudio.HeavyMachinery.ForkliftController>();
                break;
        }



        AS = GetComponent<AudioSource>();

        TotalWrongAmount = 0;

        _pipeDetectControl.Init();

        //計算訊息框顯示次數
        TimeScore = 0;
        pipeFallScore = 0;
        ForkitOnLineScore = 0;
        SpeedToHightScore = 0;
        ForkPositionHightScore = 0;
        ForkMastTiltScore = 0;
        ForkHandBrakeScore = 0;
        ForkCluthScore = 0;
        ForkBackFrontNorStopScore = 0;
        OnRoadNotEngineScore = 0;
        GoodsOnShelfOK_C = 1;
        ForkInsertGoods_C = 1;
        AlmostTouch_C = 1;
        GoodsAlmostTouch_C = 1;// by lin @　2023-11-17


        isTimeUp = false;
        isCheckOnRoadLine = false;
        isCheckStopTooLong = false;
        isCheckToHightSpeed = false;
        isCheckPositionHight = false;
        isChecMastTilt = false;
        isCheckForkHandBrake = false;
        isCheckForkCluth = false;
        isCheckBackFront = false;
        isCheckOnRoadNotEngine = false;
        //======第三關=================
        isCheckTouchShelf = false;
        isCheckToFarTo後扶架 = false;
        isCheckGoodTouchGroundLine = false;
        isCheckGoodTouchFloor = false;
        //lin liang yu add @ 2023-10-25
        isForkliftAlmostTouch = false;
        isGoodsAlmostTouch = false;// by lin @　2023-11-17
        isForkInsertGoods = false;

        


        logtichControl = GameObject.FindObjectOfType<LogtichControl>();

    }

    public void ScoreUpdate()
    {
        //用來在inspector 看數字用的
        CurrentMastTilt_ins = _forkliftController.CurrentMastTilt;
        CurrentPositionHight_ins = _forkliftController.CurrentForksVertical;
        wrongAmount_ins = ScoreGroupComp.WrongAmount;

        if (mainGameManager != null)
        {
            //=====第二關判斷項目==========
            if (mainGameManager.CurrentState == MainGameStateControl.GameFlowState.DriveForkKit)
            {
                //撞到柱子或壓線
                //超速駕駛
                //行駛時未拉高貨插
                //行駛時未傾斜貨插
                //行駛時手煞車未放
                //行駛時誤踩吋動踏板
                //行駛時突然變換前後檔
                //行駛時熄火

                if (logtichControl.CheckEnterUI || Input.GetKeyDown(KeyCode.Return)) startTime = true;//按下Enter開始計時 or 羅技上的Enter
                if (startTime) CountTime(480, 20);//8分鐘
                PipeFall(10);
                ForkitOnRoad(10);
                OnStopTooLong(2, 20);
                SpeedToHight(20, 10);
                ForkPositionHight(2, 0.052f, 5);//PositionHight_ins
                ForkMastTiltRotate(2, 0.57f, 5); //MastTilt_ins
                ForkHandBrake(2, 10);
                ForkCluth(2, 10, 10);
                ForkBackFrontNorStop(2, 10);
                OnRoadNotEngine(2, 10);
            }
        }
        //=====第三關判斷項目==========
        else if (mainGameManager_stageThree != null)
        {
            //撞到柱子或壓線
            //超速駕駛
            //行駛時未拉高貨插
            //行駛時未傾斜貨插
            //行駛時手煞車未放
            //行駛時誤踩吋動踏板
            //行駛時突然變換前後檔
            //行駛時熄火
            //================第三關新增================
            //V倉儲架裝卸作業時碰撞貨架或碰撞貨物
            //V棧板與貨(後)扶架距離超過10cm(如果要放貨物 退後時程式會誤判，所以利用特定區域不判斷解決)
            //V地面貨物區置放完成後壓線
            //V貨物置放於地面調整
            //================第三關新增by Lin Liang Yu===========
            //v 請注意堆高機快要撞到柱子或壓線
            //V 判斷貨物插到貨叉上

            if (logtichControl.CheckEnterUI || Input.GetKeyDown(KeyCode.Return)) startTime = true;//按下Enter開始計時 or 羅技上的Enter
            if (startTime) CountTime(900, 20);//15分鐘
            PipeFall(10);
            ForkitOnRoad(10);
            OnStopTooLong(2, 20);
            SpeedToHight(20, 10);

            //在貨物附近時，不判斷貨插位置是否正確
            if (StageThreeGameManager.Instance.IsNeerGoods == true)
            {
                ForkPositionHight_IsNeerGoods(2, 0.052f, 5); //PositionHight_ins
                ForkMastTiltRotate_IsNeerGoods(2, 0.57f, 5);  //MastTilt_ins
                // IsNeerGoods 這個bool 有問題，在貨物旁邊的時候，沒有進行判斷
            }
            else if (StageThreeGameManager.Instance.IsNeerGoods == false)
            {
                ForkPositionHight(2, 0.052f, 5); //PositionHight_ins
                ForkMastTiltRotate(2, 0.57f, 5); //MastTilt_ins
            }
            ForkHandBrake(2, 10);
            ForkCluth(2, 10, 10);
            ForkBackFrontNorStop(2, 10);
            OnRoadNotEngine(2, 10);
            //=====================第三關=========================
            ForkitTouchShelf(10);
            ForkGoodsTo後扶架(2, 0.02f, 5);
            GoodsTouchGroundLine(5);
            OnGoodsTouchGroundAdj(5);
            //================第三關新增by Lin Liang Yu===========
            IfAlmostTouch();  //Goods Almost Touch 也寫在裡面
            ForkInsertGoods();
            GoodsOnShelfOK();

        }

        /*void Update()
        {

        }*/



    }

    public void ReleaseEvent()
    {
        OnTimeScore = null;
        OnPipeFallScore = null;
        OnForkitOnLineScore = null;
        OnStopTooLongScore = null;
        OnSpeedToHightScore = null;
        OnForkPositionHightScore = null;
        OnForkMastTiltScore = null;
        OnForkHandBrakeScore = null;
        OnForkCluthScore = null;
        OnForkBackFrontNorStopScore = null;
        OnOnRoadNotEngineScore = null;
    }

    /// <summary>
    /// 計時
    /// </summary>
    /// <param name="limitSec"></param>
    void CountTime(int limitSec, int score)
    {
        Timer += Time.deltaTime;
        if (Timer > limitSec && isTimeUp == false)
        {
            isTimeUp = true;
            TimeScore = 1;
            OnTimeScore(TimeScore);
            PlayWrongVoice(wrongVoice[0]);
            TotalWrongAmount += score;
        }
    }

    int temp;
    /// <summary>
    /// 判斷柱子
    /// </summary>
    void PipeFall(int score)
    {
        temp = pipeFallScore;
        pipeFallScore = _pipeDetectControl.BeColliderTotalAmount;

        if (pipeFallScore > 0 && temp != pipeFallScore)
        {
            OnPipeFallScore(pipeFallScore);
            PlayWrongVoice(wrongVoice[1]);
            TotalWrongAmount += score;

            Debug.Log(TotalWrongAmount + "+++++++++++++++" + pipeFallScore);
        }
    }

    /// <summary>
    /// 壓線
    /// </summary>
    /// <param name="score"></param>
    void ForkitOnRoad(int score)
    {
        if (MainGameManager.Instance.IsForkitOnRoadOutLine && !isCheckOnRoadLine)
        {
            isCheckOnRoadLine = true;
            ForkitOnLineScore += 1;
            Debug.Log("壓線" + ForkitOnLineScore + "次");
            OnForkitOnLineScore(ForkitOnLineScore);
            PlayWrongVoice(wrongVoice[9]);
            TotalWrongAmount += score;
        }
        else if (!MainGameManager.Instance.IsForkitOnRoadOutLine)
        {
            isCheckOnRoadLine = false;
        }
    }

    /// <summary>
    /// 快要壓線
    /// </summary>
    void IfAlmostTouch()
    {
        if (MainGameManager.isAlmostTouch_B == true && isForkliftAlmostTouch == false)
        {
            OnAlmostTouch(AlmostTouch_C);
            PlayWrongVoice(wrongVoice[14]);
            isForkliftAlmostTouch = true;
            AlmostTouch_C += 1;
        }
        else if (MainGameManager.isAlmostTouch_B == false)
        {
            //isCheckOnRoadLine = false;
            isForkliftAlmostTouch = false;
        }

        if (MainGameManager.isGoodsAlmostTouch_B == true && isGoodsAlmostTouch == false)
        {
            OnGoodsAlmostTouch(GoodsAlmostTouch_C);
            PlayWrongVoice(wrongVoice[16]);
            isGoodsAlmostTouch = true;
            GoodsAlmostTouch_C += 1;
        }
        else if (MainGameManager.isGoodsAlmostTouch_B == false)
        {
            //isCheckOnRoadLine = false;
            isGoodsAlmostTouch = false;
        }
    }

    /// <summary>
    /// 插入棧板
    /// </summary>
    void ForkInsertGoods()
    {
        if (IfGoodsOnMe.isGoodsOnMe_fk == true && isForkInsertGoods == false)
        {
            OnForkInsertGoods(ForkInsertGoods_C);
            PlayWrongVoice(wrongVoice[15]);  //ding
            isForkInsertGoods = true;
            ForkInsertGoods_C += 1;
            //return;
        }
        else if (IfGoodsOnMe.isGoodsOnMe_fk == false && isForkInsertGoods == true)
        {
            //isCheckOnRoadLine = false;
            isForkInsertGoods = false;
        }
    }

    /// <summary>
    /// 貨物可以放到貨架上
    /// </summary>
    void GoodsOnShelfOK()
    {
        if (IfGoodsOnShelf.isGoodsOnMe_sf == true && isGoodsOnShelfOK == false)  // && GoodsOnShelfOK_C == 0
        {
            if (StartPoint.isOnStartPoint_Forkift == false)
            {
                OnGoodsOnShelfOK(GoodsOnShelfOK_C);
                PlayWrongVoice(wrongVoice[15]);  //ding
                isGoodsOnShelfOK = true;
                GoodsOnShelfOK_C += 1;
            }

            //return;
        }
        else if (IfGoodsOnShelf.isGoodsOnMe_sf == false && isGoodsOnShelfOK == true)
        {
            //isCheckOnRoadLine = false;
            isGoodsOnShelfOK = false;
        }

        /*else if (IfGoodsOnShelf.isGoodsOnMe_sf == false && isGoodsOnShelfOK == true)
        {
            //isCheckOnRoadLine = false;
            isGoodsOnShelfOK = false;            
        }*/

        /*if (IfGoodsOnShelf.isGoodsOnMe_sf == true && isGoodsOnShelfOK == false)    // && GoodsOnShelfOK_C >= 2
        {
            OnGoodsOnShelfOK(GoodsOnShelfOK_C);
            PlayWrongVoice(wrongVoice[15]);  //ding
            isGoodsOnShelfOK = true;
            GoodsOnShelfOK_C += 1;
        }*/

    }

    /// <summary>
    /// 原地停超過5秒
    /// </summary>
    bool isMove = false;
    float countTime = 0;
    void OnStopTooLong(float stopSpeed, int score)
    {
        //====第二關==========
        if (mainGameManager != null)
        {
            EndPoint _endPoint = MainGameManager.Instance.EndPointObjs.GetComponent<EndPoint>();

            //在開始點與終點不計算
            if (!mainGameManager.StartPointObjs.GetComponent<StartPoint>().isOnStartPoint_Forkit && !_endPoint.isOnEndPoint_Forkit)
            {
                if (_wSMVehicleController.CurrentSpeed >= stopSpeed)
                {
                    isMove = true;
                }
                else
                {
                    isMove = false;
                }
                if (isMove)
                {
                    if (_wSMVehicleController.CurrentSpeed <= 0.001f)
                    {
                        countTime += Time.deltaTime;

                    }
                    if (_wSMVehicleController.CurrentSpeed <= 0.001f && !isCheckStopTooLong && countTime > 5)
                    {
                        isCheckStopTooLong = true;
                        StopTooLongScore += 1;
                        Debug.Log("停留超過5秒" + StopTooLongScore + "次");
                        OnStopTooLongScore(StopTooLongScore);
                        //PlayWrongVoice(wrongVoice[2]);
                        TotalWrongAmount += score;
                    }
                    else if (_wSMVehicleController.CurrentSpeed > 0.001f && isCheckStopTooLong)
                    {
                        countTime = 0;
                        isCheckStopTooLong = false;
                    }
                }

            }

        }

        //====第三關==========
        if (mainGameManager_stageThree != null)
        {

            //在開始點與終點不計算
            if (!mainGameManager_stageThree.StartPointObjs.GetComponent<StartPoint>().isOnStartPoint_Forkit)
            {
                if (_wSMVehicleController.CurrentSpeed >= stopSpeed)
                {
                    isMove = true;
                }
                else
                {
                    isMove = false;
                }
                if (isMove)
                {
                    if (_wSMVehicleController.CurrentSpeed <= 0.001f)
                    {
                        countTime += Time.deltaTime;

                    }
                    if (_wSMVehicleController.CurrentSpeed <= 0.001f && !isCheckStopTooLong && countTime > 5)
                    {
                        isCheckStopTooLong = true;
                        StopTooLongScore += 1;
                        Debug.Log("停留超過5秒" + StopTooLongScore + "次");
                        OnStopTooLongScore(StopTooLongScore);
                        //PlayWrongVoice(wrongVoice[2]);
                        TotalWrongAmount += score;
                    }
                    else if (_wSMVehicleController.CurrentSpeed > 0.001f && isCheckStopTooLong)
                    {
                        countTime = 0;
                        isCheckStopTooLong = false;
                    }
                }

            }
        }

    }

    /// <summary>
    /// 超過速限後+1，如果回到規定速限以下再次超速再+1
    /// </summary>
    /// <param name="limitSpeed"></param>
    void SpeedToHight(int limitSpeed, int score)
    {
        if (_wSMVehicleController.CurrentSpeed >= limitSpeed && !isCheckToHightSpeed)
        {
            isCheckToHightSpeed = true;
            SpeedToHightScore += 1;
            Debug.Log("超過速限" + limitSpeed + "__" + SpeedToHightScore + "次");
            OnSpeedToHightScore(SpeedToHightScore);
            PlayWrongVoice(wrongVoice[2]);
            TotalWrongAmount += score;

        }
        else if (_wSMVehicleController.CurrentSpeed < limitSpeed - 5 && isCheckToHightSpeed)
        {
            isCheckToHightSpeed = false;
        }
    }

    /// <summary>
    /// 當移動速度超過speed且貨插高度位置不正確+1，當貨插上升至指定高度後，如果再次將貨插下降至指定高度以下且移動速度超過speed則再次+1
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="forkHight"></param>
    void ForkPositionHight(float speed, float forkHight, int score)
    {
        //Debug.Log("=============_forkliftController.CurrentForksVertical"+_forkliftController.CurrentForksVertical);

        //Debug.Log("CurrentForksVertical:"+_forkliftController.CurrentForksVertical);
        if (_wSMVehicleController.CurrentSpeed > speed &&
            _forkliftController.CurrentForksVertical <= forkHight //0.052f
            && isCheckPositionHight == false)
        {
            isCheckPositionHight = true;
            ForkPositionHightScore += 1;
            Debug.Log(_forkliftController.CurrentForksVertical + "貨插位置錯誤_高度" + ForkPositionHightScore + "次");
            OnForkPositionHightScore(ForkPositionHightScore);
            PlayWrongVoice(wrongVoice[3]);
            TotalWrongAmount += score;

        }
        else if (_forkliftController.CurrentForksVertical > forkHight && isCheckPositionHight == true)
        {
            isCheckPositionHight = false;
        }
    }

    /// <summary>
    /// 當移動速度超過speed且貨插高度傾斜角度不正確+1，當貨插傾斜至指定角度後，如果再次將貨插傾斜至指定角度以外且移動速度超過speed則再次+1
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="tiltRotate"></param>
    void ForkMastTiltRotate(float speed, float tiltRotate, int score)
    {
        //Debug.Log("_CurrentMastTilt" + _forkliftController.CurrentMastTilt);

        if (_wSMVehicleController.CurrentSpeed > speed &&
            _forkliftController.CurrentMastTilt >= tiltRotate //0.57f
            && isChecMastTilt == false)
        {
            isChecMastTilt = true;
            ForkMastTiltScore += 1;
            Debug.Log(_forkliftController.CurrentMastTilt + "貨插位置錯誤_傾斜角度" + ForkMastTiltScore + "次");
            OnForkMastTiltScore(ForkMastTiltScore);
            PlayWrongVoice(wrongVoice[4]);
            TotalWrongAmount += score;

        }
        else if (_forkliftController.CurrentMastTilt < tiltRotate && isChecMastTilt == true)
        {
            isChecMastTilt = false;
        }
    }

    /// <summary>
    /// 當移動速度超過speed且手剎車拉起則+1，將手煞車放下後，如果再次手剎車拉起且移動速度超過speed則再次+1
    /// </summary>
    /// <param name="speed"></param>
    void ForkHandBrake(float speed, int score)
    {
        //Debug.Log("=======_wSMVehicleController.CurrentSpeed " + _wSMVehicleController.CurrentSpeed);
        if (_wSMVehicleController.CurrentHandbrake == 1 &&
            _wSMVehicleController.CurrentSpeed > speed &&
            !isCheckForkHandBrake)
        {
            ForkHandBrakeScore += 1;
            isCheckForkHandBrake = true;
            OnForkHandBrakeScore(ForkHandBrakeScore);
            PlayWrongVoice(wrongVoice[5]);
            TotalWrongAmount += score;

        }
        else if (_wSMVehicleController.CurrentHandbrake == 0 && isCheckForkHandBrake)
        {
            isCheckForkHandBrake = false;
        }

    }


    //void ForkBrake(float speed,int brake)
    //{
    //    if (_wSMVehicleController.CurrentBrakes >= brake &&
    //        _wSMVehicleController.CurrentSpeed > speed &&
    //        !isCheckBrake)
    //    {
    //        ForkBrakeScore += 1;
    //        isCheckBrake = true;
    //    }
    //    else if (_wSMVehicleController.CurrentBrakes < brake && isCheckBrake)
    //    {
    //        isCheckBrake = false;
    //    }
    //}

    /// <summary>
    /// 當移動速度超過speed且壓下吋動踏板則+1，將吋動踏板釋放，如果再次壓下吋動踏板且移動速度超過speed則再次+1
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="clutch"></param>
    void ForkCluth(float speed, int clutch, int score)
    {
        if (_wSMVehicleController.CurrentClutch >= clutch &&
          _wSMVehicleController.CurrentSpeed > speed &&
          !isCheckForkCluth)
        {
            ForkCluthScore += 1;
            isCheckForkCluth = true;
            OnForkCluthScore(ForkCluthScore);
            PlayWrongVoice(wrongVoice[6]);
            TotalWrongAmount += score;

        }
        else if (_wSMVehicleController.CurrentClutch < clutch && isCheckForkCluth)
        {
            isCheckForkCluth = false;
        }
    }

    /// <summary>
    /// 當移動速度超過speed且切換前/後/空檔則+1
    /// </summary>
    /// <param name="speed"></param>
    float lastFrameFrontBack = 0;
    void ForkBackFrontNorStop(float speed, int score)
    {
        if (_wSMVehicleController.CurrentBackFront != lastFrameFrontBack &&
        _wSMVehicleController.CurrentSpeed > speed &&
        !isCheckBackFront)
        {
            ForkBackFrontNorStopScore += 1;
            isCheckBackFront = true;
            OnForkBackFrontNorStopScore(ForkBackFrontNorStopScore);
            PlayWrongVoice(wrongVoice[7]);
            TotalWrongAmount += score;

        }
        else if (_wSMVehicleController.CurrentBackFront == lastFrameFrontBack && isCheckBackFront)
        {
            isCheckBackFront = false;
        }

        lastFrameFrontBack = _wSMVehicleController.CurrentBackFront;
    }

    /// <summary>
    /// 當移動速度超過speed且引擎熄火則+1，將引擎開啟後，如果再次引擎熄火且移動速度超過speed則再次+1
    /// </summary>
    /// <param name="speed"></param>
    void OnRoadNotEngine(float speed, int score)
    {
        if (_wSMVehicleController.CurrentEngineOn == false &&
        _wSMVehicleController.CurrentSpeed > speed &&
        !isCheckOnRoadNotEngine)
        {
            OnRoadNotEngineScore += 1;
            isCheckOnRoadNotEngine = true;
            OnOnRoadNotEngineScore(OnRoadNotEngineScore);
            PlayWrongVoice(wrongVoice[8]);
            TotalWrongAmount += score;

        }
        else if (_wSMVehicleController.CurrentEngineOn == true && isCheckOnRoadNotEngine)
        {
            isCheckOnRoadNotEngine = false;
        }
    }


    /// <summary>
    /// 第三關，撞擊貨架
    /// </summary>
    /// <param name="score"></param>
    void ForkitTouchShelf(int score)
    {
        if (StageThreeGameManager.Instance.IsForkitTouchShelf && !isCheckTouchShelf)
        {
            isCheckTouchShelf = true;
            ForkitTouchShelfScore += 1;
            Debug.Log("撞擊貨架" + ForkitTouchShelfScore + "次");
            OnForkiTouchShelfScore(ForkitTouchShelfScore);
            PlayWrongVoice(wrongVoice[10]);
            TotalWrongAmount += score;
        }
        else if (!StageThreeGameManager.Instance.IsForkitTouchShelf)
        {
            isCheckTouchShelf = false;
        }
    }


    /// <summary>
    /// 第三關，當在貨物附近 當移動速度超過speed且貨物距離後扶架超過10cm，倒車時不判斷
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="forkHight"></param>
    void ForkGoodsTo後扶架(float speed, float forkHight, int score)
    {
        //Debug.Log("_forkliftController.CurrentForksVertical:" + _forkliftController.CurrentForksVertical);
        //貨物距離後扶架超過10cm
        if (StageThreeGameManager.Instance.IsTooFarTo後扶架 &&
            !isCheckToFarTo後扶架 &&
            _wSMVehicleController.CurrentSpeed > speed &&
            _wSMVehicleController.BackFrontInput != -1 &&//倒車不判斷
            _forkliftController.CurrentForksVertical > forkHight)//貨架要升起
        {
            isCheckToFarTo後扶架 = true;
            ForkitToFarto後扶架Score += 1;
            Debug.Log("貨物距離後扶架超過10cm" + ForkitToFarto後扶架Score + "次");
            OnForkitToFarTo後扶架Score(ForkitToFarto後扶架Score);
            TotalWrongAmount += score;
            PlayWrongVoice(wrongVoice[12]);

        }
        else if (!StageThreeGameManager.Instance.IsTooFarTo後扶架)
        {
            isCheckToFarTo後扶架 = false;
        }
    }

    /// <summary>
    ///  第三關，地面貨物區置放完成後壓線
    /// </summary>
    /// <param name="score"></param>
    void GoodsTouchGroundLine(int score)
    {
        if (StageThreeGameManager.Instance.IsTouchGroundLine &&
           !isCheckGoodTouchGroundLine)
        {
            isCheckGoodTouchGroundLine = true;
            GoodTouchGroundLineScore += 1;
            Debug.Log("地面貨物區置放完成後壓線" + GoodTouchGroundLineScore + "次");
            OnGoodTouchGroundLineScore(GoodTouchGroundLineScore);
            TotalWrongAmount += score;
            PlayWrongVoice(wrongVoice[13]);
        }
        else if (!StageThreeGameManager.Instance.IsTouchGroundLine)
        {
            isCheckGoodTouchGroundLine = false;
        }
    }

    /// <summary>
    ///  第三關，裝卸作業時，貨叉上棧板歪斜，貨物置放於地面調整。
    /// </summary>
    /// <param name="score"></param>
    void OnGoodsTouchGroundAdj(int score)
    {
        if (StageThreeGameManager.Instance.IsGoodsTouchFloor &&
            !isCheckGoodTouchFloor)
        {
            isCheckGoodTouchFloor = true;
            GoodTouchFloorScore += 1;
            Debug.Log("貨物置放於地面調整" + GoodTouchFloorScore + "次");
            OnGoodTouchFloorScore(GoodTouchFloorScore);
            TotalWrongAmount += score;
            PlayWrongVoice(wrongVoice[11]);
        }
        else if (!StageThreeGameManager.Instance.IsGoodsTouchFloor)
        {
            isCheckGoodTouchFloor = false;
        }



    }
    /// <summary>
    /// 第三關，靠近貨物時對貨叉的判斷
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="tiltRotate"></param>
    /// <param name="score"></param>
    void ForkPositionHight_IsNeerGoods(float speed, float forkHight, int score)
    {
        //不判斷
    }


    /// <summary>
    /// 第三關，靠近貨物時對貨叉的判斷
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="tiltRotate"></param>
    void ForkMastTiltRotate_IsNeerGoods(float speed, float tiltRotate, int score)
    {
        //不判斷

    }


    void PlayWrongVoice(AudioClip clip)
    {
        AS.clip = clip;
        AS.Stop();
        AS.Play();
    }
}

using UnityEngine;
using UnityEngine.Events;

namespace WSMGameStudio.HeavyMachinery
{
    [RequireComponent(typeof(ForkliftController))]
    public class ForkliftPlayerInput : MonoBehaviour
    {
        [HideInInspector]
        public LogtichControl logtichControl;

        public bool enablePlayerInput = true;
        public ForkliftInputSettings inputSettings;
        public UnityEvent[] customEvents;

        private ForkliftController _forkliftController;
        private Vehicles.WSMVehicleController _WSMVehicleController;


        private int _mastTilt = 0;
        private int _forksVertical = 0;
        //private int _forksHorizontal = 0;
        private int _backFront = 0;
        private int _changeLight = 0;

        bool isChageControlMode = false;


        /// <summary>
        /// Initializing references
        /// </summary>
        void Start()
        {
            _forkliftController = GetComponent<ForkliftController>();

            _WSMVehicleController = GetComponent<Vehicles.WSMVehicleController>();

            logtichControl = GetComponent<LogtichControl>();

        }

        /// <summary>
        /// Handling player input
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                isChageControlMode = !isChageControlMode;
            }


            if (isChageControlMode)
            {
                OnEditTest();
            }
            else
            {
                if (enablePlayerInput)
                {
                    LogichUse();
                }
            }
        }

        void LogichUse()
        {
            if (inputSettings == null) return;

            #region Forklift Controls

            if (Input.GetKeyDown(inputSettings.toggleEngine))
                _forkliftController.IsEngineOn = !_forkliftController.IsEngineOn;

            ////////////////////////////////////X Y 軸
            if (JoyStickHelper.GetAxisPosition(JoyStickHelper.ControlDirection.X)>-0.3f)
            {
            }


            //貨插
            //_mastTilt = logtichControl.MastTiltForwards ? 1 : logtichControl.MastTiltBackwards ? -1 : 0;
            //_forksVertical = logtichControl.ForkUp ? 1 : logtichControl.ForkDown ? -1 : 0;

            //Pot 2
            // if (GetAllJoysEvent.DegreeBar_Rz_傾斜 < 0.0f) _mastTilt = 1;
            // if (GetAllJoysEvent.DegreeBar_Rz_傾斜 < 0.14f && GetAllJoysEvent.DegreeBar_Rz_傾斜 > 0.0f) _mastTilt = 0;
            // if (GetAllJoysEvent.DegreeBar_Rz_傾斜 > 0.14f) _mastTilt = -1;
            // //Pot 1
            // if (GetAllJoysEvent.UpDownBar_Ry_升降 < 0.17f) _forksVertical = -1;
            // if (GetAllJoysEvent.UpDownBar_Ry_升降 < 0.41f && GetAllJoysEvent.UpDownBar_Ry_升降 > 0.17f) _forksVertical = 0;
            // if (GetAllJoysEvent.UpDownBar_Ry_升降 > 0.41f) _forksVertical = 1;
            // 
            // Debug.Log("pot1:" + ArduinoPort.instance.pot1);
            // Debug.Log("pot2:" + ArduinoPort.instance.pot2);
            //
            // GetAllJoysEvent.UpDownBar_Ry_升降 = _forksVertical;
            // GetAllJoysEvent.DegreeBar_Rz_傾斜 = _mastTilt;

            //Pot 2
            if (GetAllJoysEvent.DegreeBar_Rz_傾斜 < 0) _mastTilt = 1;
            if (GetAllJoysEvent.DegreeBar_Rz_傾斜 ==0) _mastTilt = 0;
            if (GetAllJoysEvent.DegreeBar_Rz_傾斜 > 0)
            {
                if (GetAllJoysEvent.DegreeBar_Rz_傾斜 < 3f)
                {
                    _mastTilt = 0;
                }
                else if (GetAllJoysEvent.DegreeBar_Rz_傾斜 >= 3f)
                {
                    _mastTilt = -1;
                    Debug.Log("Hi" + GetAllJoysEvent.DegreeBar_Rz_傾斜);
                }
            }
            //_mastTilt = -1;
            //Pot 1
            if (GetAllJoysEvent.UpDownBar_Ry_升降 < 0) {
                if (GetAllJoysEvent.UpDownBar_Ry_升降 > -2)
                {
                    _forksVertical = 0;
                }
                else if (GetAllJoysEvent.UpDownBar_Ry_升降 <= -2)
                {
                    _forksVertical = -1;
                    //Debug.Log("Hi" + GetAllJoysEvent.DegreeBar_Rz_傾斜);
                }
            }
            
            if (GetAllJoysEvent.UpDownBar_Ry_升降 ==0) _forksVertical = 0;
            if (GetAllJoysEvent.UpDownBar_Ry_升降 >0) _forksVertical = 1;

            //改羅技
            //if (logtichControl.BackMove) _backFront = 1;
            //if (!logtichControl.FrontMove && !logtichControl.BackMove) _backFront = 0;
            //if (logtichControl.FrontMove) _backFront = -1;
            //if (Input.GetKey(inputSettings.backMove)) _backFront = -1;
            //if (Input.GetKey(inputSettings.nullMove)) _backFront = 0;
            //if (Input.GetKey(inputSettings.frontMove)) _backFront = 1;////////////////////////////
            if (GetAllJoysEvent.FrontBar_btn6) _backFront = -1;
            if (!GetAllJoysEvent.FrontBar_btn5 && !GetAllJoysEvent.FrontBar_btn6) _backFront = 0;
            if (GetAllJoysEvent.FrontBar_btn5) _backFront = 1;



            if (Input.GetKey(inputSettings.rightLight) && _changeLight != 1) _changeLight = 1;
            if (Input.GetKey(inputSettings.nullLight) && _changeLight != 0) _changeLight = 0;
            if (Input.GetKey(inputSettings.leftLight) && _changeLight != -1) _changeLight = -1;


            _forkliftController.RotateMast(-_mastTilt);//貨插旋轉
            _forkliftController.MoveTilt(-_mastTilt);//液壓白鐵前後
            _forkliftController.MoveForksVertically(-_forksVertical);//貨插升降
            _WSMVehicleController.CurrenLightControl(_changeLight);//更換燈


            //操作桿做動
            _forkliftController.UpdateLevers(_forksVertical, _mastTilt, _backFront, _changeLight);//(_forksVertical, _forksHorizontal, _mastTilt)



            #endregion

            #region Player Custom Events

            for (int i = 0; i < inputSettings.customEventTriggers.Length; i++)
            {
                if (Input.GetKeyDown(inputSettings.customEventTriggers[i]))
                {
                    if (customEvents.Length > i)
                        customEvents[i].Invoke();
                }
            }

            #endregion
        }

        void OnEditTest()
        {
            if (inputSettings == null) return;

            #region Forklift Controls

            if (Input.GetKeyDown(inputSettings.toggleEngine))
                _forkliftController.IsEngineOn = !_forkliftController.IsEngineOn;

            _mastTilt = Input.GetKey(inputSettings.mastTiltForwards) ? 1 : (Input.GetKey(inputSettings.mastTiltBackwards) ? -1 : 0);

            _forksVertical = Input.GetKey(inputSettings.forksUp) ? 1 : (Input.GetKey(inputSettings.forksDown) ? -1 : 0);

            //_forksHorizontal = Input.GetKey(inputSettings.forksRight) ? 1 : (Input.GetKey(inputSettings.forksLeft) ? -1 : 0);
            if (Input.GetKey(inputSettings.backMove)) _backFront = -1;
            if (Input.GetKey(inputSettings.nullMove)) _backFront = 0;
            if (Input.GetKey(inputSettings.frontMove)) _backFront = 1;



            if (Input.GetKey(inputSettings.rightLight) && _changeLight != 1) _changeLight = 1;
            if (Input.GetKey(inputSettings.nullLight) && _changeLight != 0) _changeLight = 0;
            if (Input.GetKey(inputSettings.leftLight) && _changeLight != -1) _changeLight = -1;

            //_backFront = Input.GetKey(inputSettings.backMove) ? 1 : (Input.GetKey(inputSettings.frontMove) ? -1 : (Input.GetKey(inputSettings.nullMove) ?0:0));
            //_changePower = Input.GetKey(inputSettings.rightLight) ? 1 : (Input.GetKey(inputSettings.leftLight) ?2 : 1);


            _forkliftController.RotateMast(_mastTilt);//貨插旋轉
            _forkliftController.MoveTilt(_mastTilt);//液壓白鐵前後
            _forkliftController.MoveForksVertically(_forksVertical);//貨插升降
                                                                    //更換燈
            _WSMVehicleController.CurrenLightControl(_changeLight);

            //_forkliftController.MoveForksHorizontally(_forksHorizontal);

            //操作桿做動
            _forkliftController.UpdateLevers(_forksVertical, _mastTilt, _backFront, _changeLight);//(_forksVertical, _forksHorizontal, _mastTilt)



            #endregion

            #region Player Custom Events

            for (int i = 0; i < inputSettings.customEventTriggers.Length; i++)
            {
                if (Input.GetKeyDown(inputSettings.customEventTriggers[i]))
                {
                    if (customEvents.Length > i)
                        customEvents[i].Invoke();
                }
            }

            #endregion
        }

    }
}

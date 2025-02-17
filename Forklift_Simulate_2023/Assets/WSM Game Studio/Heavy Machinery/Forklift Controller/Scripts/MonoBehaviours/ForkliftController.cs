﻿using UnityEngine;

namespace WSMGameStudio.HeavyMachinery
{
    [System.Serializable]
    public class ForkliftController : MonoBehaviour
    {
        public float forksVerticalSpeed = 0.1f;
        public float forksHorizontalSpeed = 0.5f; //not this
        public float mastTiltSpeed = 0.3f;

        [SerializeField] private bool _isEngineOn = true;

        [SerializeField] public RotatingMechanicalPart mainMast;
        [SerializeField] public MovingMechanicalPart secondaryMast;
        [SerializeField] public MovingMechanicalPart forksCylinders;
        [SerializeField] public MovingMechanicalPart forks;
        [SerializeField] public MovingMechanicalPart tiltPipe01;

        [SerializeField] public Transform forksVerticalLever;
        //[SerializeField] public Transform forksHorizontalLever;
        [SerializeField] public Transform mastTiltLever;
        [SerializeField] public Transform Lever_BackFront;
        [SerializeField] public Transform Lever_ChangePower;
        [SerializeField] public AudioSource forkMovingSFX;
        [SerializeField] public AudioSource forkStartMovingSFX;
        [SerializeField] public AudioSource forkStopMovingSFX;

        private float _verticalLeverAngle = 0;
        //private float _horizontalLeverAngle = 0;
        private float _tiltLeverAngle = 0;
        private float _Lever_BackFrontAngle = 0;
        private float _Lever_ChangePowerAngle = 0;
        
        private float forksVerAddSpeed;
        private float mastTiltAddSpeed;


        [Range(0f, 1f)] private float _forksVertical;
        [Range(0f, 1f)] private float _secondaryMastVertical;
        [Range(0f, 1f)] private float _forksHorizontal;
        [Range(0f, 1f)] private float _mastTilt;
        [Range(0f, 1f)] private float _tiltPipe01;
        [Range(0f, 1f)] private float _tiltPipe02;

        /// <summary>
        /// 給外部用
        /// </summary>
        public float ForksVertical
        {
            get { return _forksVertical; }
            set { _forksVertical = value; }
        }
        public float ForksHorizontal
        {
            get { return _forksHorizontal; }
            set { _forksHorizontal = value; }
        }
        public float MastTilt
        {
            get { return _mastTilt; }
            set { _mastTilt = value; }
        }

        public bool IsEngineOn
        {
            get { return _isEngineOn; }
            set
            {
                if (!_isEngineOn && value)
                    StartEngine();
                else if (_isEngineOn && !value)
                    StopEngine();
            }
        }

        public float CurrentForksVertical { get { return _forksVertical; } }
        public float CurrentMastTilt { get { return _mastTilt; } }
 

        /// <summary>
        /// Initialize forklift
        /// </summary>
        private void Start()
        {
            _mastTilt = mainMast.MovementInput;
            _forksHorizontal = forksCylinders.MovementInput;
            _forksVertical = forks.MovementInput;
            _secondaryMastVertical = secondaryMast.MovementInput;
            _tiltPipe01 = tiltPipe01.MovementInput;

            forksVerticalSpeed = 0.01f; //初始值
            mastTiltSpeed = 0.03f;

            forksVerticalSpeed = Mathf.Abs(forksVerticalSpeed);
            forksHorizontalSpeed = Mathf.Abs(forksHorizontalSpeed);
            mastTiltSpeed = Mathf.Abs(mastTiltSpeed);
        }

        /// <summary>
        /// 
        /// </summary>
        private void LateUpdate()
        {
            if (_isEngineOn)
            {
                bool forksMoving = forks.IsMoving || secondaryMast.IsMoving || mainMast.IsMoving || forksCylinders.IsMoving;
                ForksMovementSFX(forksMoving); //Should be called on late update to track SFX correctly
            }
        }

        /// <summary>
        /// Starts vehicle engine
        /// </summary>
        public void StartEngine()
        {
            _isEngineOn = true;
        }

        /// <summary>
        /// Stop vehicle engine
        /// </summary>
        public void StopEngine()
        {
            _isEngineOn = false;
        }

        /// <summary>
        /// Handles forks vertical movement
        /// </summary>
        /// <param name="verticalInput">-1 = down | 0 = none | 1 = up</param>
        public void MoveForksVertically(int verticalInput)
        {
            if (_isEngineOn)
            {
                forksVerAddSpeed = Mathf.Abs(GetAllJoysEvent.pot1);
                forksVerAddSpeed = Mathf.Sqrt(forksVerAddSpeed); //開根號
                /*forksVerticalSpeed = forksVerticalSpeed * forksVerticalSpeed * 0.004f;*/

                //_forksVertical += _secondaryMastVertical <= 0 ? (verticalInput * Time.deltaTime * forksVerticalSpeed) : 1f;
                _forksVertical += (verticalInput * Time.deltaTime * forksVerticalSpeed* forksVerAddSpeed*0.55f) ;
                _forksVertical = Mathf.Clamp01(_forksVertical);

                //_secondaryMastVertical += _forksVertical >= 1 ? (verticalInput * Time.deltaTime * forksVerticalSpeed) : 0f;
                //_secondaryMastVertical = Mathf.Clamp01(_secondaryMastVertical);

                forks.MovementInput = _forksVertical;
                secondaryMast.MovementInput = _forksVertical;
            }
        }

        /// <summary>
        /// Handles forks horizontal movement
        /// </summary>
        /// <param name="horizontalInput">-1 = left | 0 = none | 1 = right</param>
        public void MoveForksHorizontally(int horizontalInput)
        {
            if (_isEngineOn)
            {
                _forksHorizontal += (horizontalInput * Time.deltaTime * forksHorizontalSpeed);
                _forksHorizontal = Mathf.Clamp01(_forksHorizontal);
                forksCylinders.MovementInput = _forksHorizontal; 
            }
        }

        /// <summary>
        /// Handles mast rotation
        /// </summary>
        /// <param name="direction">-1 = backwards | 0 = none | 1 = forward</param>
        public void RotateMast(int direction)
        {
            if (_isEngineOn)
            {

                mastTiltAddSpeed = Mathf.Abs(GetAllJoysEvent.pot2); //絕對值
                mastTiltAddSpeed = Mathf.Sqrt(mastTiltAddSpeed); //開根號

                _mastTilt += (direction * Time.deltaTime * mastTiltSpeed * mastTiltAddSpeed * 0.28f);
                _mastTilt = Mathf.Clamp01(_mastTilt);
                mainMast.MovementInput = _mastTilt; 
            }
        }

        public void MoveTilt(int verticalInput)
        {
            float speed = 0.4f;
            if (_isEngineOn)
            {
                _tiltPipe01 += (verticalInput * Time.deltaTime * speed);

                _tiltPipe01 = Mathf.Clamp01(_tiltPipe01);

                tiltPipe01.MovementInput = _tiltPipe01;
            }
        }

        /// <summary>
        /// Animate levers accordingly to player's input
        /// </summary>
        /// <param name="forkVerticalInput"></param>
        /// <param name="forkHorizontalInput"></param>
        /// <param name="mastTiltInput"></param>
        public void UpdateLevers(int forkVerticalInput, int mastTiltInput,int Lever_BackFrontInput, int Lever_ChangeLightInput)//(int forkVerticalInput, int forkHorizontalInput, int mastTiltInput)
        {
            if (_isEngineOn)
            {
                _verticalLeverAngle = Mathf.MoveTowards(_verticalLeverAngle, forkVerticalInput * -10f, 70f * Time.deltaTime);
                //_horizontalLeverAngle = Mathf.MoveTowards(_horizontalLeverAngle, forkHorizontalInput * 10f, 70f * Time.deltaTime);
                _tiltLeverAngle = Mathf.MoveTowards(_tiltLeverAngle, mastTiltInput * 10f, 70f * Time.deltaTime);
                _Lever_BackFrontAngle = Mathf.MoveTowards(_Lever_BackFrontAngle, Lever_BackFrontInput * 10f, 70f * Time.deltaTime); //10f是旋轉範圍
                _Lever_ChangePowerAngle = Mathf.MoveTowards(_Lever_ChangePowerAngle, -Lever_ChangeLightInput * 10f, 70f * Time.deltaTime);

                //Debug.Log("Lever_BackFrontInput:" + Lever_BackFrontInput);
                //Debug.Log("_Lever_BackFrontAngle" + _Lever_BackFrontAngle + "  _Lever_ChangePowerAngle" + _Lever_ChangePowerAngle);

                if (forksVerticalLever != null) forksVerticalLever.localEulerAngles = new Vector3(180f, _verticalLeverAngle+90, 20f);
                //if (forksHorizontalLever != null) forksHorizontalLever.localEulerAngles = new Vector3(_horizontalLeverAngle, 0f, 0f);
                if (mastTiltLever != null) mastTiltLever.localEulerAngles = new Vector3(180f, _tiltLeverAngle + 90, 20f);
                if (Lever_BackFront != null) Lever_BackFront.localEulerAngles = new Vector3(_Lever_BackFrontAngle + 90f, 90f, -90);//+ 105f,
                if (Lever_ChangePower != null) Lever_ChangePower.localEulerAngles = new Vector3(_Lever_ChangePowerAngle - 105f, 90f, 90);


            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="play"></param>
        private void ForksMovementSFX(bool forksMoving)
        {
            if (_isEngineOn && forkMovingSFX != null)
            {
                if (!forkMovingSFX.isPlaying && forksMoving)
                {
                    forkMovingSFX.Play();

                    if (forkStartMovingSFX != null && !forkStartMovingSFX.isPlaying)
                        forkStartMovingSFX.Play();
                }
                else if (forkMovingSFX.isPlaying && !forksMoving)
                {
                    forkMovingSFX.Stop();

                    if (forkStopMovingSFX != null && !forkStopMovingSFX.isPlaying)
                        forkStopMovingSFX.Play();
                }
            }
        }
    }
}

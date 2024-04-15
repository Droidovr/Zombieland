using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.UIModule;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterPhysicMovingPC : MonoBehaviour, ICharacterPhysicMoving
    {
        private const float GRAVITY = 9.8f;
        private const float ROTATION_SMOOTH_TIME = 0.03f;
        private const float MIN_VECTORMOVE_MAGITUDE = 0.1f;
        private const float DEFAULT_SPEED_MULTIPLIER = 1f;
        private const float FAST_SPEED_MULTIPLIER = 3f;

        private Vector2 _vectorMove;
        private Vector2 _vectorMousePosition;
        private float _verticalSpeed;
        private UnityEngine.CharacterController _unityCharacterController;
        private IUIMain _uIController;
        private ICharacterDataController _characterDataController;
        private ICharacterMovingController _characterMovingController;
        public bool _isActive;
        private float _speedMultiplier = 1f;
        private Vector2 _centerScreen;
        private Vector2 _defaultSizeCursor = new Vector2(32f, 32f);


        #region PUBLIC
        public void Disable()
        {
            _uIController.OnMoved -= MovedHandler;
            _uIController.OnMouseMoved -= MovedMouseHandler;
            _characterMovingController.CharacterController.AnimationController.OnAnimatorMove -= OnAnimatorMoveHandler;
        }

        public void Init(ICharacterMovingController characterMovingController)
        {
            _unityCharacterController = GetComponent<UnityEngine.CharacterController>();

            _characterMovingController = characterMovingController;
            _characterMovingController.CharacterController.AnimationController.OnAnimatorMove += OnAnimatorMoveHandler;

            _uIController = characterMovingController.CharacterController.RootController.UIController;
            _uIController.OnMoved += MovedHandler;
            _uIController.OnMouseMoved += MovedMouseHandler;
            _uIController.OnFastRun += FastRunHandler;

            _characterDataController = characterMovingController.CharacterController.CharacterDataController;

            _isActive = true;
            _centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
        }

        public void ActivateMoving(bool isActive)
        {
            _unityCharacterController.enabled = isActive;
            _isActive = isActive;
        }
        #endregion PUBLIC

        #region MONOBEHAVIOUR
        private void Update()
        {
            if (!_isActive)
                return;

            CalculateGravity();

            CalculeteRealMovingSpeed();

            if (_vectorMousePosition.magnitude > MIN_VECTORMOVE_MAGITUDE)
            {
                CalculeteRotation();
            }
        }
        #endregion


        #region PRIVATE
        private void OnAnimatorMoveHandler(Vector3 animatorDeltaPosition)
        {
            if (_unityCharacterController.enabled)
            {
                _unityCharacterController.Move(animatorDeltaPosition);
            }
        }

        private void MovedHandler(Vector2 joystickPosition)
        {
            _vectorMove = joystickPosition;
        }

        private void MovedMouseHandler(Vector2 mousePosition)
        {
            _vectorMousePosition = mousePosition;
        }

        private void CalculateGravity()
        {
            if (_unityCharacterController.enabled)
            {
                _verticalSpeed -= _unityCharacterController.isGrounded ? _verticalSpeed : GRAVITY * Time.deltaTime;
                _unityCharacterController.Move(Vector3.up * _verticalSpeed * Time.deltaTime);
            }
        }

        private void FastRunHandler(bool isFastRun)
        {
            _speedMultiplier = isFastRun ? FAST_SPEED_MULTIPLIER : DEFAULT_SPEED_MULTIPLIER;
        }

        private void CalculeteRealMovingSpeed()
        {
            _characterMovingController.DirectionWalk = new Vector3(_vectorMove.x, 0f, _vectorMove.y);
            _characterMovingController.RealMovingSpeed = Mathf.Clamp01(_characterMovingController.DirectionWalk.magnitude) * _characterDataController.CharacterData.DesignMovingSpeed * _speedMultiplier;
        }

        private void CalculeteRotation()
        {
            Vector2 cursorCenter = _vectorMousePosition - _defaultSizeCursor/2;

            Vector2 offset = cursorCenter - _centerScreen;
            float angle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _characterDataController.CharacterData.DesignRotationSpeed);
            transform.rotation = targetRotation;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5f);
        }
        #endif

        #endregion PRIVATE
    }
}
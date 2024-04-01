using UnityEngine;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.UIModule;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterPhysicMovingPC : MonoBehaviour, ICharacterPhysicMoving
    {
        private const float GRAVITY = 9.8f;
        private const float ROTATION_SMOOTH_TIME = 0.03f;
        private const float MIN_VECTORMOVE_MAGITUDE = 0.1f;

        private Vector2 _vectorMove;
        private Vector2 _vectorMousePosition;
        private float _verticalSpeed;
        private UnityEngine.CharacterController _unityCharacterController;
        private IUIMain _uIController;
        private ICharacterDataController _characterDataController;
        private ICharacterMovingController _characterMovingController;
        public bool _isActive;
        private float _speedMultiplier = 1f;


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

        private void FastRunHandler(bool isShift)
        {
            _speedMultiplier = isShift ? 3 : 1;
        }

        private void CalculeteRealMovingSpeed()
        {
            _characterMovingController.DirectionWalk = new Vector3(_vectorMove.x, 0f, _vectorMove.y);

            _characterMovingController.RealMovingSpeed = Mathf.Clamp01(_characterMovingController.DirectionWalk.magnitude) * _characterDataController.CharacterData.DesignMovingSpeed * _speedMultiplier;

            if (Time.frameCount % 60 == 0)
            {
                Debug.Log(_characterMovingController.RealMovingSpeed);
            }
        }

        private void CalculeteRotation()
        {
            Ray ray = _characterMovingController.CharacterController.RootController.CameraController.PlayerCamera.ScreenPointToRay(_vectorMousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 mousePositionOnScene = hit.point;
                mousePositionOnScene.y = transform.position.y;
                Vector3 direction = mousePositionOnScene - transform.position;
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float rotationSpeed = _characterDataController.CharacterData.DesignRotationSpeed;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, ROTATION_SMOOTH_TIME);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
        #endregion PRIVATE
    }
}
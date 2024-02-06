using UnityEngine;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.UIModule;


namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterPhysicMoving : MonoBehaviour
    {
        public float RealMovingSpeed { get; private set; }

        private const float GRAVITY = 9.8f;
        private const float ROTATION_SMOOTH_TIME = 0.03f;
        private const float MIN_VECTORMOVE_MAGITUDE = 0.1f;

        private Vector2 _vectorMove;
        private float _verticalSpeed;
        private UnityEngine.CharacterController _unityCharacterController;
        private IUIMain _uIController;
        private ICharacterDataController _characterDataController;


        #region MONOBEHAVIOUR
        private void Update()
        {
            CalculateGravity();

            CalculeteRealMovingSpeed();

            if (_vectorMove.magnitude > MIN_VECTORMOVE_MAGITUDE)
            {
                CalculeteRotation();
            }
        }
        #endregion


        #region PUBLIC
        public void Disable()
        {
            _uIController.OnMoved -= HandleMoved;
        }

        public void Initialize(ICharacterMovingController characterMovingController)
        {
            _unityCharacterController = GetComponent<UnityEngine.CharacterController>();

            _uIController = characterMovingController.CharacterController.RootController.UIController;
            _uIController.OnMoved += HandleMoved;

            _characterDataController = characterMovingController.CharacterController.CharacterDataController;
        }
        #endregion PUBLIC


        #region PRIVATE
        private void CalculateGravity()
        {
            if (_unityCharacterController.enabled)
            {
                _verticalSpeed += _unityCharacterController.isGrounded ? GRAVITY : -GRAVITY;
                _unityCharacterController.Move(Vector3.up * _verticalSpeed * Time.deltaTime);
            }
        }

        private void CalculeteRealMovingSpeed()
        {
            Vector3 movementDirection = new Vector3(_vectorMove.x, 0f, _vectorMove.y);
            RealMovingSpeed = Mathf.Clamp01(movementDirection.magnitude) * _characterDataController.CharacterData.DesignMovingSpeed;
        }

        private void CalculeteRotation()
        {
            Vector3 direction = new Vector3(_vectorMove.x, 0f, _vectorMove.y).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float rotationSpeed = _characterDataController.CharacterData.DesignRotationSpeed;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, ROTATION_SMOOTH_TIME);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        private void HandleMoved(Vector2 joystickPosition)
        {
            _vectorMove = joystickPosition;
        }
        #endregion PRIVATE
    }
}
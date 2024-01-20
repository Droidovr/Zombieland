using UnityEngine;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.UIModule;


namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterPhysicMoving : MonoBehaviour
    {
        public float RealMovingSpeed => _characterController.velocity.magnitude;

        private const float GRAVITY = 9.8f;
        private const float MIN_VECTORMOVE_MAGNITUDE = 0.1f;
        private const float ROTATION_SMOOTH_TIME = 0.03f;

        private Vector2 _vectorMove;
        private float _verticalSpeed;
        private UnityEngine.CharacterController _characterController;
        private IUIMain _uIController;
        private ICharacterDataController _characterDataController;


        #region MONOBEHAVIOUR

        private void Update()
        {
            CalculateGravity();

            if (_vectorMove.magnitude > MIN_VECTORMOVE_MAGNITUDE)
            {
                Movement();
            }
        }

        public void Disable()
        {
            _uIController.OnMoved -= HandleMoved;
        }
        #endregion


        #region PUBLIC
        public void Initialize(ICharacterMovingController characterMovingController)
        {
            _characterController = GetComponent<UnityEngine.CharacterController>();

            _uIController = characterMovingController.CharacterController.RootController.UIController;
            _uIController.OnMoved += HandleMoved;

            _characterDataController = characterMovingController.CharacterController.CharacterDataController;
        }
        #endregion PUBLIC


        #region PRIVATE
        private void CalculateGravity()
        {
            _verticalSpeed += _characterController.isGrounded ? GRAVITY : -GRAVITY;
            _characterController.Move(Vector3.up * _verticalSpeed * Time.deltaTime);
        }

        private void Movement()
        {
            Vector3 direction = new Vector3(_vectorMove.x, 0f, _vectorMove.y).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float rotationSpeed = _characterDataController.CharacterData.DesignRotationSpeed;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, ROTATION_SMOOTH_TIME);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            _characterController.Move(moveDirection * _characterDataController.CharacterData.DesignMovingSpeed * Time.deltaTime);
        }

        private void HandleMoved(Vector2 joystickPosition)
        {
            _vectorMove = joystickPosition;
        }
        #endregion PRIVATE
    }
}
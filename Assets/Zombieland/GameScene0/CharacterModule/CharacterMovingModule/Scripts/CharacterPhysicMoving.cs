using UnityEngine;
using Zombieland.GameScene0.UIModule;


namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterPhysicMoving : MonoBehaviour
    {
        private const float GRAVITY = 9.8f;

        private float _movingSpeed;
        private float _rotationSpeed;
        private Vector2 _vectorMove;
        private float _verticalSpeed;
        private UnityEngine.CharacterController _characterController;
        private IUIMain _uIController;

        private float _smoothTime = 0.1f;


        #region MONOBEHAVIOUR

        private void Update()
        {
            CalculateGravity();

            if (_vectorMove.magnitude > _smoothTime)
            {
                Movement();
            }
        }

        private void OnDestroy()
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

            // Get Data CharacterDataController
            _movingSpeed = 5;
            _rotationSpeed = 5;
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
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationSpeed, _smoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            _characterController.Move(moveDirection * _movingSpeed * Time.deltaTime);
        }

        private void HandleMoved(Vector2 joystickPosition)
        {
            _vectorMove = joystickPosition;
        }
        #endregion PRIVATE
    }
}
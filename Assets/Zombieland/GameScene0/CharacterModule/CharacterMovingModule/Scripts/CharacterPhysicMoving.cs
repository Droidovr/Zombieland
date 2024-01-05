using System;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterPhysicMoving : MonoBehaviour
    {
        public float MovingSpeed
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Moving speed cannot be negative.", nameof(MovingSpeed));
                }

                _movingSpeed = value;
            }
        }
        public float MovingRotation
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Moving rotation cannot be negative.", nameof(MovingRotation));
                }

                _movingRotation = value;
            }
        }
        public float Gravity
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Gravity cannot be negative.", nameof(Gravity));
                }

                _gravity = value;

            }
        }
        public CharacterMovingController CharacterMovingController
        {
            set
            {
                _characterMovingController = value;
            }
        }


        private float _movingSpeed;
        private float _movingRotation;
        private float _gravity;
        private Vector2 _vectorMove;
        private float _verticalSpeed;
        private CharacterMovingController _characterMovingController;
        private UnityEngine.CharacterController _characterController;

        private float _smoothTime = 0.1f;


        #region MONOBEHAVIOUR

        private void Update()
        {
            CalculateGravity();

            if (_vectorMove.magnitude < _smoothTime)
                return;

            Movement();
        }
        #endregion


        #region PUBLIC
        public void Initialize()
        {
            _characterController = GetComponent<UnityEngine.CharacterController>();
            _characterMovingController.OnJoustickMoved += HandleJoystickMoved;
        }
        #endregion PUBLIC


        #region PRIVATE
        private void HandleJoystickMoved(Vector2 joystickPosition)
        {
            _vectorMove = joystickPosition;
        }

        private void CalculateGravity()
        {
            if (!_characterController.isGrounded)
            {
                _verticalSpeed -= _gravity * Time.deltaTime;
            }
            else
            {
                _verticalSpeed = -_gravity * Time.deltaTime;
            }
        }

        private void Movement()
        {
            Vector3 direction = new Vector3(_vectorMove.x, 0f, _vectorMove.y).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _movingRotation, _smoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            moveDirection.y += _verticalSpeed;
            _characterController.Move(moveDirection * _movingSpeed * Time.deltaTime);
        }
        #endregion PRIVATE
    }
}
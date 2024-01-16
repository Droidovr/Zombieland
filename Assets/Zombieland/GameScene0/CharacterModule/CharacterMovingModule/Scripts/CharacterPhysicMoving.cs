using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterPhysicMoving : MonoBehaviour
    {
        private float _movingSpeed;
        private float _movingRotation;
        private float _gravity;
        private Vector2 _vectorMove;
        private float _verticalSpeed;
        private UnityEngine.CharacterController _characterController;

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
        #endregion


        #region PUBLIC
        public void Initialize(CharacterMovingController characterMovingController, float movingSpeed,float movingRotation, float gravity)
        {
            _characterController = GetComponent<UnityEngine.CharacterController>();
            characterMovingController.OnMoved += HandleMoved;

            _movingSpeed = movingSpeed;
            _movingRotation = movingRotation;
            _gravity = gravity;
        }
        #endregion PUBLIC


        #region PRIVATE
        private void HandleMoved(Vector2 joystickPosition)
        {
            _vectorMove = joystickPosition;
        }

        private void CalculateGravity()
        {
            _verticalSpeed += _characterController.isGrounded ? _gravity : -_gravity;
            _characterController.Move(Vector3.up * _verticalSpeed * Time.deltaTime);
        }

        private void Movement()
        {
            Vector3 direction = new Vector3(_vectorMove.x, 0f, _vectorMove.y).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _movingRotation, _smoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            _characterController.Move(moveDirection * _movingSpeed * Time.deltaTime);
        }
        #endregion PRIVATE
    }
}
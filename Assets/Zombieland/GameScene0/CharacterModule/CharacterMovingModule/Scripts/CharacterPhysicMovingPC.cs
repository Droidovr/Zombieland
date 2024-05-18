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
        private const float DEFAULT_SPEED_MULTIPLIER = 1f;
        private const float FAST_SPEED_MULTIPLIER = 3f;

        private Vector2 _vectorMousePosition;
        private float _verticalSpeed;
        private UnityEngine.CharacterController _unityCharacterController;
        private ICharacterMovingController _characterMovingController;
        public bool _isActive;
        private float _speedMultiplier = 1f;
        private float _unityCharacterControllerHeight;
        private Vector3 _unityCharacterControllerCenter;


        #region PUBLIC
        public void Disable()
        {
            _characterMovingController.CharacterController.AnimationController.OnAnimationMove -= OnAnimatorMoveHandler;
            _characterMovingController.CharacterController.RootController.UIController.OnMoved -= MovedHandler;
            _characterMovingController.CharacterController.RootController.UIController.OnMouseMoved -= MovedMouseHandler;
            _characterMovingController.CharacterController.AnimationController.OnAnimationMove -= OnAnimatorMoveHandler;
            _characterMovingController.CharacterController.StealthController.OnStealth -= StealthHandler;
        }

        public void Init(ICharacterMovingController characterMovingController)
        {
            _characterMovingController = characterMovingController;

            _unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            _unityCharacterControllerHeight = _unityCharacterController.height;
            _unityCharacterControllerCenter = _unityCharacterController.center;

            _characterMovingController.CharacterController.AnimationController.OnAnimationMove += OnAnimatorMoveHandler;
            _characterMovingController.CharacterController.RootController.UIController.OnMoved += MovedHandler;
            _characterMovingController.CharacterController.RootController.UIController.OnMouseMoved += MovedMouseHandler;
            _characterMovingController.CharacterController.RootController.UIController.OnFastRun += FastRunHandler;
            _characterMovingController.CharacterController.StealthController.OnStealth += StealthHandler;

            _isActive = true;
        }

        public void ActivateMoving(bool isActive)
        {
            _unityCharacterController.enabled = isActive;
            _isActive = isActive;
        }
        #endregion PUBLIC

        #region MONOBEHAVIOUR
        private void FixedUpdate()
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
            int x = Mathf.RoundToInt(joystickPosition.x);
            int y = Mathf.RoundToInt(joystickPosition.y);

            Vector2 vectorMove = new Vector2(x, y);

            if (Mathf.Abs(vectorMove.x) != 0f)
            {
                vectorMove.y = 0f;
            }

            _characterMovingController.DirectionWalk = vectorMove;
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
            _characterMovingController.RealMovingSpeed = Mathf.Clamp01(_characterMovingController.DirectionWalk.magnitude) * 
                _characterMovingController.CharacterController.CharacterDataController.CharacterData.DesignMovingSpeed * _speedMultiplier;
        }

        private void CalculeteRotation()
        {
            Vector2 centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Vector2 offset = _vectorMousePosition - centerScreen;
            float angle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = targetRotation;
        }

        private void StealthHandler(bool isStealth)
        {
            if (isStealth)
            {
                _unityCharacterController.height = _unityCharacterControllerHeight * 0.75f;
                _unityCharacterController.center = new Vector3(_unityCharacterControllerCenter.x, _unityCharacterControllerCenter.y * 0.75f, _unityCharacterControllerCenter.z);
            }
            else
            {
                _unityCharacterController.height = _unityCharacterControllerHeight;
                _unityCharacterController.center = _unityCharacterControllerCenter;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5f);

            if (_characterMovingController.CharacterController.CharacterWeaponController.WeaponPointFire != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(_characterMovingController.CharacterController.CharacterWeaponController.WeaponPointFire.position,
                    _characterMovingController.CharacterController.CharacterWeaponController.WeaponPointFire.position +
                    _characterMovingController.CharacterController.CharacterWeaponController.WeaponPointFire.forward * 5f);
            }
        }
#endif

        #endregion PRIVATE
    }
}
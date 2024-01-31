using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterAnimator : MonoBehaviour
    {
        private const float DAMP_TIME = 0.05f;
        
        private Animator _animator;
        private ICharacterController _characterController;
        private UnityEngine.CharacterController _unityEngineCharacterController;

        private void Update()
        {
            _animator.SetFloat("RealMovingSpeed", _characterController.CharacterMovingController.RealMovingSpeed, DAMP_TIME, Time.deltaTime);

            if (_characterController.CharacterMovingController.RealMovingSpeed > 0)
            {
                _animator.SetBool("IsMoving", true);
            }
            else 
            {
                _animator.SetBool("IsMoving", false);
            }
        }

        public void Init(ICharacterController CharacterController)
        {
            _animator = GetComponent<Animator>();
            _characterController = CharacterController;
            _unityEngineCharacterController = GetComponent<UnityEngine.CharacterController>();
        }

        private void OnAnimatorMove()
        {
            Vector3 velocity = _animator.deltaPosition;
            if (_unityEngineCharacterController.enabled)
                _unityEngineCharacterController.Move(velocity);
        }
    }
}

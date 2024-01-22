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
            float speed = _characterController.CharacterMovingController.RealMovingSpeed;

            _animator.SetFloat("RealMovingSpeed", speed, DAMP_TIME, Time.deltaTime);

            if (speed > 0)
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
            _unityEngineCharacterController.Move(velocity);
        }
    }
}

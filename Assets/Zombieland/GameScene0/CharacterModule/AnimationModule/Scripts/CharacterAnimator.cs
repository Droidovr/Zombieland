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
        }

        public void Init(ICharacterController CharacterController)
        {
            _animator = GetComponent<Animator>();
            _characterController = CharacterController;
            _unityEngineCharacterController = GetComponent<UnityEngine.CharacterController>();
        }

        private void OnAnimatorMove()
        {
            if (_unityEngineCharacterController.enabled)
            {
                _unityEngineCharacterController.Move(_animator.deltaPosition);
            }
        }
    }
}

using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterAnimator : MonoBehaviour
    {
        public Animator Animator { get; private set; }
        
        private const float DAMP_TIME = 0.05f;
        
        private ICharacterController _characterController;

        private void Update()
        {
            Animator.SetFloat("RealMovingSpeed", _characterController.CharacterMovingController.RealMovingSpeed, DAMP_TIME, Time.deltaTime);
        }

        public void Init(ICharacterController CharacterController)
        {
            Animator = GetComponent<Animator>();
            _characterController = CharacterController;
        }
    }
}

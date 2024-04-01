using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterAnimator : MonoBehaviour
    {
        public event Action<Vector3> OnAnimatorMoveHandler;

        private const string PC_ANIMATOR = "PCAnimatorController";
        private const string MOBILE_ANIMATOR = "Character0MobileAnimator";
        private const float DAMP_TIME = 0.05f;
        
        private ICharacterController _characterController;
        private Animator _animator;

        private void Update()
        {
            _animator.SetFloat("RealMovingSpeed", _characterController.CharacterMovingController.RealMovingSpeed, DAMP_TIME, Time.deltaTime);
            _animator.SetFloat("DirectionX", _characterController.CharacterMovingController.DirectionWalk.x, DAMP_TIME, Time.deltaTime);
            _animator.SetFloat("DirectionZ", _characterController.CharacterMovingController.DirectionWalk.z, DAMP_TIME, Time.deltaTime);
        }

        public void Init(ICharacterController CharacterController)
        {
            _animator = GetComponent<Animator>();

#if UNITY_STANDALONE || UNITY_EDITOR
            _animator.runtimeAnimatorController = Resources.Load<AnimatorController>(PC_ANIMATOR);
#else
            _animator.runtimeAnimatorController = Resources.Load<AnimatorController>(MOBILE_ANIMATOR);
#endif

            _characterController = CharacterController;
        }

        private void OnAnimatorMove()
        {
            if (_animator.enabled)
            {
                OnAnimatorMoveHandler?.Invoke(_animator.deltaPosition);
            }
        }
    }
}

using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterAnimator : MonoBehaviour
    {
        public event Action<Vector3> OnAnimatorMoveHandler;
        public event Action OnFinish;
        public event Action OnFinishPreparationAttack;

        private const string PC_ANIMATOR = "PCAnimatorController";
        private const string MOBILE_ANIMATOR = "Character0MobileAnimator";
        private const float DAMP_TIME = 0.05f;

        private ICharacterController _characterController;
        private Animator _animator;

        private void Update()
        {
            _animator.SetFloat("RealMovingSpeed", _characterController.CharacterMovingController.RealMovingSpeed, DAMP_TIME, Time.deltaTime);

            Vector3 moveDirection = transform.InverseTransformDirection(_characterController.CharacterMovingController.DirectionWalk);

            _animator.SetFloat("DirectionX", moveDirection.x, DAMP_TIME, Time.deltaTime);
            _animator.SetFloat("DirectionZ", moveDirection.z, DAMP_TIME, Time.deltaTime);
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

            //_characterController.EquipmentController. += WeaponChangeHandler;
        }

        public void Disable()
        {
            //_characterController.EquipmentController. -= WeaponChangeHandler;
        }

        public void FinishHandler()
        {
            OnFinish?.Invoke();
            Debug.Log("FinishHandler");
        }

        private void FinishPreparationAttackHandler()
        {
            OnFinishPreparationAttack?.Invoke();
            Debug.Log("FinishPreparationAttackHandler");
        }

        private void WeaponChangeHandler()
        { 
            
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

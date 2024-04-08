using System;
using UnityEditor.Animations;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;
using Zombieland.GameScene0.ImpactModule;

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

        private IAnimationController _animatorController;
        private Animator _animator;

        public void Init(IAnimationController animatorController)
        {
            _animator = GetComponent<Animator>();

#if UNITY_STANDALONE || UNITY_EDITOR
            _animator.runtimeAnimatorController = Resources.Load<AnimatorController>(PC_ANIMATOR);
#else
            _animator.runtimeAnimatorController = Resources.Load<AnimatorController>(MOBILE_ANIMATOR);
#endif

            _animatorController = animatorController;

            _animatorController.CharacterController.EquipmentController.OnWeaponChanged += WeaponChangeHandler;
            _animatorController.CharacterController.StealthController.OnStealth += StealthHandler;
            _animatorController.CharacterController.RootController.UIController.OnFire += FireHandler;

            //Test
            TestEquipment testEquipment = new TestEquipment(_animatorController);
            testEquipment.OnWeaponChanged += WeaponChangeHandler;
        }

        public void Disable()
        {
            _animatorController.CharacterController.EquipmentController.OnWeaponChanged -= WeaponChangeHandler;
            _animatorController.CharacterController.StealthController.OnStealth -= StealthHandler;
            _animatorController.CharacterController.RootController.UIController.OnFire -= FireHandler;
        }

        public void FinishHandler()
        {
            OnFinish?.Invoke();
            Debug.Log("FinishHandler");
        }

        private void Update()
        {
            _animator.SetFloat("RealMovingSpeed", _animatorController.CharacterController.CharacterMovingController.RealMovingSpeed, DAMP_TIME, Time.deltaTime);

            Vector3 moveDirection = transform.InverseTransformDirection(_animatorController.CharacterController.CharacterMovingController.DirectionWalk);

            _animator.SetFloat("DirectionX", moveDirection.x, DAMP_TIME, Time.deltaTime);
            _animator.SetFloat("DirectionZ", moveDirection.z, DAMP_TIME, Time.deltaTime);
        }

        private void FinishPreparationAttackHandler()
        {
            OnFinishPreparationAttack?.Invoke();
            Debug.Log("FinishPreparationAttackHandler");
        }

        private void WeaponChangeHandler(Weapon weapon)
        { 
            string nameWeapon = weapon.WeaponData.Name;

            _animator.SetBool("IsWrench", false);
            _animator.SetBool("IsPistol", false);
            _animator.SetBool("IsShotgun", false);

            switch (nameWeapon)
            {
                case "Wrench":
                    _animator.SetBool("IsWrench", true);
                    break;

                case "Pistol":
                    _animator.SetBool("IsPistol", true);
                    break;

                case "Shotgun":
                    _animator.SetBool("IsShotgun", true);
                    break;

                default:
                    break;
            }
        }

        private void StealthHandler(bool isStealth)
        {
            if (isStealth)
            {
                _animator.SetBool("IsStealth", true);
            }
            else
            {
                _animator.SetBool("IsStealth", false);
            }
        }

        private void FireHandler(bool isFire)
        { 
            if (!isFire && _animator.GetBool("IsWrench") || _animator.GetBool("IsPistol") || _animator.GetBool("IsShotgun") )
            {
                _animator.SetTrigger("Attack");
                Debug.Log("FireHandler");
            }
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

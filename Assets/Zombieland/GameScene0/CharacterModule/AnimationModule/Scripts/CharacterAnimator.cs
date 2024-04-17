using System;
using UnityEditor.Animations;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterAnimator : MonoBehaviour
    {
        public event Action<Vector3> OnAnimatorMoveHandler;
        public event Action<string> OnStartWeaponAnimation;
        public event Action<string> OnFinishWeaponAnimation;
        public event Action OnFinishPreparationAttack;

        private const string PC_ANIMATOR = "PCAnimatorController";
        private const string MOBILE_ANIMATOR = "Character0MobileAnimator";
        private const float DAMP_TIME = 0.05f;

        private IAnimationController _animatorController;
        private Animator _animator;
        private bool _isWeaponAnimation = false;
        private string _nameWeapon;

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

            ////Test
            //TestEquipment testEquipment = new TestEquipment(_animatorController);
            //testEquipment.OnWeaponChanged += WeaponChangeHandler;
        }

        public void Disable()
        {
            _animatorController.CharacterController.EquipmentController.OnWeaponChanged -= WeaponChangeHandler;
            _animatorController.CharacterController.StealthController.OnStealth -= StealthHandler;
            _animatorController.CharacterController.RootController.UIController.OnFire -= FireHandler;
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
        }

        private void FinishHandler(string nameFinishWeapon)
        {
            OnFinishWeaponAnimation?.Invoke(nameFinishWeapon);
            ChangeWeaponAnimation(_nameWeapon);
        }

        private void WeaponChangeHandler(Weapon weapon)
        { 
            _nameWeapon = weapon.WeaponData.Name;

            Debug.Log(_nameWeapon);

            _animator.SetBool("IsWrench", false);
            _animator.SetBool("IsPistol", false);
            _animator.SetBool("IsShotgun", false);

            if (!_isWeaponAnimation)
            {
                ChangeWeaponAnimation(_nameWeapon);
            }
        }

        private void ChangeWeaponAnimation(string nameWeapon)
        {            
            OnStartWeaponAnimation?.Invoke(nameWeapon);

            switch (nameWeapon)
            {
                case "Wrench":
                    _animator.SetBool("IsWrench", true);
                    _isWeaponAnimation = true;
                    break;

                case "Pistol":
                    _animator.SetBool("IsPistol", true);
                    _isWeaponAnimation = true;
                    break;

                case "Shotgun":
                    _animator.SetBool("IsShotgun", true);
                    _isWeaponAnimation = true;
                    break;

                default:
                    _isWeaponAnimation = false;
                    _nameWeapon = null;
                    break;
            }
        }

        private void StealthHandler(bool isStealth)
        {
            _animator.SetBool("IsStealth", isStealth);
        }

        private void FireHandler(bool isFire)
        {
            _animator.SetBool("Attack", isFire);
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

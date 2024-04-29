using System;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterAnimator : MonoBehaviour
    {
        public event Action<Vector3> OnAnimationMove;
        public event Action<bool> OnAnimationAttack;
        public event Action<string> OnAnimationCreateWeapon;
        public event Action OnAnimationDestroyWeapon;
        public event Action OnStep;

        private const string PC_ANIMATOR = "PCAnimatorController";
        private const string MOBILE_ANIMATOR = "MobileAnimatorController";
        private const float DAMP_TIME = 0.05f;
        private const float CHECK_FIRE_PERMITION_PERIOD = 0.1f;

        private IAnimationController _animatorController;
        private Animator _animator;
        private bool _isWeaponAnimation = false;
        private Weapon _weapon;
        private FirePermiser _firePermiser;

        public void Init(IAnimationController animatorController)
        {
            _animator = GetComponent<Animator>();

#if UNITY_STANDALONE || UNITY_EDITOR
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(PC_ANIMATOR);
#else
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(MOBILE_ANIMATOR);
#endif

            _animatorController = animatorController;

            _animatorController.CharacterController.EquipmentController.OnWeaponChanged += WeaponChangeHandler;
            _animatorController.CharacterController.StealthController.OnStealth += StealthHandler;
            _animatorController.CharacterController.RootController.UIController.OnFire += UIFireHandler;

            _firePermiser = new FirePermiser(_animatorController);

            ////Test
            //TestEquipment testEquipment = new TestEquipment(_animatorController);
            //testEquipment.OnWeaponChanged += WeaponChangeHandler;
        }

        public void Disable()
        {
            _animatorController.CharacterController.EquipmentController.OnWeaponChanged -= WeaponChangeHandler;
            _animatorController.CharacterController.StealthController.OnStealth -= StealthHandler;
            _animatorController.CharacterController.RootController.UIController.OnFire -= UIFireHandler;
        }

        private void Update()
        {
            _animator.SetFloat("RealMovingSpeed", _animatorController.CharacterController.CharacterMovingController.RealMovingSpeed, DAMP_TIME, Time.deltaTime);

            //Vector3 moveDirection = transform.InverseTransformDirection(_animatorController.CharacterController.CharacterMovingController.DirectionWalk);

            _animator.SetFloat("DirectionX", _animatorController.CharacterController.CharacterMovingController.DirectionWalk.x, DAMP_TIME, Time.deltaTime);
            _animator.SetFloat("DirectionZ", _animatorController.CharacterController.CharacterMovingController.DirectionWalk.z, DAMP_TIME, Time.deltaTime);
        }


        private void WeaponChangeHandler(Weapon weapon)
        { 
            _weapon = weapon;

            _animator.SetBool("IsWrench", false);
            _animator.SetBool("IsPistol", false);
            _animator.SetBool("IsShotgun", false);
            _animator.SetBool("IsAK", false);

            if (!_isWeaponAnimation)
            {
                ChangeWeaponAnimation();
            }
        }

        private void ChangeWeaponAnimation()
        {            
            switch (_weapon.WeaponData.Name)
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

                case "AK":
                    _animator.SetBool("IsAK", true);
                    _isWeaponAnimation = true;
                    break;

                default:
                    _isWeaponAnimation = false;
                    _weapon = null;
                    break;
            }
        }

        private void AttackHandler()
        {
            OnAnimationAttack?.Invoke(true);
        }

        private void StealthHandler(bool isStealth)
        {
            _animator.SetBool("IsStealth", isStealth);
        }

        private void UIFireHandler(bool isFire)
        {
            if (isFire)
            {
                InvokeRepeating("StartFirePermision", 0, CHECK_FIRE_PERMITION_PERIOD);
            }
            else
            {
                _animator.SetBool("Attack", isFire);
                OnAnimationAttack?.Invoke(false);
            }
        }

        private void StartFirePermision()
        {
            if (_firePermiser.CheckFirePermission(_weapon))
            {
                CancelInvoke("StartFirePermision");
                _animator.SetBool("Attack", true);
            }
        }

        private void OnAnimatorMove()
        {
            if (_animator.enabled)
            {
                OnAnimationMove?.Invoke(_animator.deltaPosition);
            }
        }

        private void CreacteWeaponPrefabHandler()
        {
            OnAnimationCreateWeapon?.Invoke(_weapon.WeaponData.PrefabName);
        }

        private void DestroyWeaponPrefabHandler()
        {
            OnAnimationDestroyWeapon?.Invoke();
        }

        private void StepHandler()
        {
            OnStep?.Invoke();
        }
    }
}
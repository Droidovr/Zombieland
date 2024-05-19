using System;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.NPCModule.NPCAnimationModule
{
    public class NPCAnimator : MonoBehaviour
    {
        public event Action<Vector3> OnAnimatorMoveEvent;
        public event Action<bool> OnAnimationAttack;
        public event Action<string> OnAnimationCreateWeapon;
        public event Action OnAnimationDestroyWeapon;
        public event Action OnStep;

        private const float DAMP_TIME = 0.05f;
        private const float CHECK_FIRE_PERMITION_PERIOD = 0.1f;

        private INPCAnimationController _nPCAnimatorController;
        private Animator _animator;
        private bool _isWeaponAnimation = false;
        private Weapon _weapon;

        public void Init(INPCAnimationController nPCAnimatorController)
        {
            _animator = GetComponent<Animator>();

#if UNITY_STANDALONE || UNITY_EDITOR
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(nPCAnimatorController.NPCController.NPCDataController.NPCData.NameAnimatorControllerPC);
#else
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(nPCAnimatorController.NPCController.NPCDataController.NPCData.NameAnimatorControllerMobile);
#endif

            _nPCAnimatorController = nPCAnimatorController;
            _nPCAnimatorController.NPCController.NPCMovingController.OnMoving += MovingHandler;

            //_nPCAnimatorController.NPCController.OnFire += FireHandler;


            _animator.SetFloat("Speed", _nPCAnimatorController.NPCController.NPCDataController.NPCData.Speed);
        }


        public void Disable()
        {
            _nPCAnimatorController.NPCController.NPCMovingController.OnMoving -= MovingHandler;
            //_nPCAnimatorController.NPCController.NPCAIController.OnFire -= FireHandler;
        }


        private void MovingHandler(float speed, bool isMove)
        {
            _animator.SetBool("IsMove", isMove);
        }

        private void AttackHandler()
        {
            OnAnimationAttack?.Invoke(true);
        }

        private void FireHandler(bool isFire)
        {
            if (_nPCAnimatorController.NPCController.NPCVisualBodyController.WeaponInScene != null)
            {
                _animator.SetBool("Attack", isFire);
                OnAnimationAttack?.Invoke(isFire);
            }
        }


        private void OnAnimatorMove()
        {
            if (_animator.enabled)
            {
                OnAnimatorMoveEvent?.Invoke(_animator.rootPosition);
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
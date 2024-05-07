using System;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.NPCModule.NPCAnimationModule
{
    public class NPCAnimator : MonoBehaviour
    {
        public event Action<Vector3> OnAnimationMove;
        public event Action<bool> OnAnimationAttack;
        public event Action<string> OnAnimationCreateWeapon;
        public event Action OnAnimationDestroyWeapon;
        public event Action OnStep;

        private const string PC_ANIMATOR_0 = "NPCAnimatorControllerPC_0";
        private const string PC_ANIMATOR_1 = "NPCAnimatorControllerPC_1";
        private const string PC_ANIMATOR_2 = "NPCAnimatorControllerPC_2";
        private const string MOBILE_ANIMATOR = "NPCAnimatorControllerPC_0";
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
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(PC_ANIMATOR_0);
#else
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(MOBILE_ANIMATOR);
#endif

            _nPCAnimatorController = nPCAnimatorController;

            //_nPCAnimatorController.NPCController.OnFire += FireHandler;

        }

        public void Disable()
        {
            //_nPCAnimatorController.NPCController.OnFire -= FireHandler;
        }

        private void Update()
        {
            //_animator.SetFloat("Speed", _nPCAnimatorController.NPCController. CharacterMovingController.RealMovingSpeed, DAMP_TIME, Time.deltaTime);

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
                OnAnimationMove?.Invoke(_animator.deltaPosition);
            }
        }

        private void StepHandler()
        {
            OnStep?.Invoke();
        }
    }
}
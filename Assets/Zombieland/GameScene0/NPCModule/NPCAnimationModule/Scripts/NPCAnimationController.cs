using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.AnimationModule;


namespace Zombieland.GameScene0.NPCModule.NPCAnimationModule
{
    public class NPCAnimationController : Controller, INPCAnimationController
    {
        public event Action<Vector3> OnAnimatorMoveEvent;
        public event Action<bool> OnAnimationAttack;
        public event Action<string> OnAnimationCreateWeapon;
        public event Action OnAnimationDestroyWeapon;
        public event Action OnStep;

        private NPCAnimator _nPCAnimator;
        private NPCRagdoll _nPCRagdoll;

        public INPCController NPCController { get; private set; }


        public NPCAnimationController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }

        public void ApplyImpactHandler(Vector3 impactCollisionPosition, Vector3 impactDirection)
        {
            _nPCRagdoll.Hit(impactCollisionPosition, impactDirection);
        }

        protected override void CreateHelpersScripts()
        {
            _nPCAnimator = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCAnimator>();
            _nPCAnimator.Init(this);
            _nPCAnimator.OnAnimatorMoveEvent += OnAnimatorMoveEventHandler;
            _nPCAnimator.OnAnimationAttack += AnimationAttackHandler;
            _nPCAnimator.OnAnimationCreateWeapon += AnimationCreateWeaponHandler;
            _nPCAnimator.OnAnimationDestroyWeapon += AnimationDestroyWeaponHandler;
            _nPCAnimator.OnStep += StepHandler;

            _nPCRagdoll = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCRagdoll>();
            _nPCRagdoll.Init(this);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        private void OnAnimatorMoveEventHandler(Vector3 animatorRootPosition)
        {
            OnAnimatorMoveEvent?.Invoke(animatorRootPosition);
        }

        private void AnimationAttackHandler(bool isFire)
        {
            OnAnimationAttack?.Invoke(isFire);
        }

        private void AnimationCreateWeaponHandler(string weaponPrefabName)
        {
            OnAnimationCreateWeapon?.Invoke(weaponPrefabName);
        }

        private void AnimationDestroyWeaponHandler()
        {
            OnAnimationDestroyWeapon?.Invoke();
        }

        private void StepHandler()
        {
            OnStep?.Invoke();
        }
    }
}
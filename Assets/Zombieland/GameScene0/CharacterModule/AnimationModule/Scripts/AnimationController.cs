using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class AnimationController : Controller, IAnimationController
    {
        public event Action<Vector3> OnAnimationMove;
        public event Action OnAnimationAttack;
        public event Action<string> OnAnimationCreateWeapon;
        public event Action OnAnimationDestroyWeapon;

        public ICharacterController CharacterController { get; private set; }

        private CharacterAnimator _characterAnimator;


        public AnimationController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public override void Disable()
        {
            _characterAnimator.OnAnimationMove -= AnimationMoveHandler;
            _characterAnimator.OnAnimationAttack -= AnimationAttackHandler;
            _characterAnimator.OnAnimationCreateWeapon -= AnimationCreateWeaponHandler;
            _characterAnimator.OnAnimationDestroyWeapon -= AnimationDestroyWeaponHandler;
            _characterAnimator.Disable();

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            GameObject character = CharacterController.VisualBodyController.CharacterInScene;

            _characterAnimator = character.AddComponent<CharacterAnimator>();
            _characterAnimator.Init(this);
            _characterAnimator.OnAnimationMove += AnimationMoveHandler;
            _characterAnimator.OnAnimationAttack += AnimationAttackHandler;
            _characterAnimator.OnAnimationCreateWeapon += AnimationCreateWeaponHandler;
            _characterAnimator.OnAnimationDestroyWeapon += AnimationDestroyWeaponHandler;

            CharacterRagdoll characterRagdoll = character.AddComponent<CharacterRagdoll>();
            characterRagdoll.Init(this);

            //Test
            //TestShooter testShooter = character.AddComponent<TestShooter>();
            //testShooter.Init(characterRagdoll);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }

        private void AnimationMoveHandler(Vector3 deltaPosition)
        {
            OnAnimationMove?.Invoke(deltaPosition);
        }

        private void AnimationAttackHandler()
        {
            OnAnimationAttack?.Invoke();
        }

        private void AnimationCreateWeaponHandler(string weaponPrefabName)
        {
            OnAnimationCreateWeapon?.Invoke(weaponPrefabName);
        }

        private void AnimationDestroyWeaponHandler()
        {
            OnAnimationDestroyWeapon?.Invoke();
        }
    }
}
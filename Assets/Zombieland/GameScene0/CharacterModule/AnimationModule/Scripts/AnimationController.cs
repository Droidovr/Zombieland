using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class AnimationController : Controller, IAnimationController
    {
        public event Action<Vector3> OnAnimatorMove;
        public event Action OnFinishPreparationAttack;

        public ICharacterController CharacterController { get; private set; }

        private CharacterAnimator _characterAnimator;


        public AnimationController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public override void Disable()
        {
            _characterAnimator.OnAnimatorMoveHandler -= AnimatorMoveHandler;
            _characterAnimator.OnFinishPreparationAttack -= FinishPreparationAttack;
            _characterAnimator.Disable();

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            GameObject character = CharacterController.VisualBodyController.CharacterInScene;

            _characterAnimator = character.AddComponent<CharacterAnimator>();
            _characterAnimator.Init(CharacterController);
            _characterAnimator.OnAnimatorMoveHandler += AnimatorMoveHandler;
            _characterAnimator.OnFinishPreparationAttack += FinishPreparationAttack;

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

        private void AnimatorMoveHandler(Vector3 deltaPosition)
        {
            OnAnimatorMove?.Invoke(deltaPosition);
        }

        private void FinishPreparationAttack()
        {
            OnFinishPreparationAttack?.Invoke();
        }
    }
}
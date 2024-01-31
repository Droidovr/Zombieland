using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class AnimationController : Controller, IAnimationController
    {
        public ICharacterController CharacterController { get; private set; }
        public Ragdoll Ragdoll { get; private set; }

        private CharacterAnimator _characterAnimator;

        public AnimationController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        protected override void CreateHelpersScripts()
        {
            GameObject character = CharacterController.VisualBodyController.CharacterInScene;
            
            character.AddComponent<CharacterAnimator>();
            _characterAnimator = character.GetComponent<CharacterAnimator>();
            _characterAnimator.Init(CharacterController);

            Ragdoll = new Ragdoll(character);

            character.AddComponent<TestShooter>();
            TestShooter testShooter = character.GetComponent<TestShooter>();
            testShooter.Init(Ragdoll);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }
    }
}
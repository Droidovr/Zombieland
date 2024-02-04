using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class AnimationController : Controller, IAnimationController
    {
        public ICharacterController CharacterController { get; private set; }

        public AnimationController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        protected override void CreateHelpersScripts()
        {
            GameObject character = CharacterController.VisualBodyController.CharacterInScene;

            CharacterAnimator characterAnimator = character.AddComponent<CharacterAnimator>();
            characterAnimator.Init(CharacterController);

            CharacterRagdoll characterRagdoll = character.AddComponent<CharacterRagdoll>();

            TestShooter testShooter = character.AddComponent<TestShooter>();
            testShooter.Init(characterRagdoll);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }
    }
}
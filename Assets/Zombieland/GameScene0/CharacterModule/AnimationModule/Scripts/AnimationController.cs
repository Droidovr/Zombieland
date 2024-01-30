using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class AnimationController : Controller, IAnimationController
    {
        public ICharacterController CharacterController { get; private set; }

        private CharacterAnimator _characterAnimator;

        //Test
        private TestRagdoll _testRagdoll;

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

            // Test
            character.AddComponent<TestRagdoll>();
            _testRagdoll = character.GetComponent<TestRagdoll>();
            _testRagdoll.Init();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }
    }
}
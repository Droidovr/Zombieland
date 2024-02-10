using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterMovingController : Controller, ICharacterMovingController
    {
        public float RealMovingSpeed { get; set; }
        public ICharacterController CharacterController { get; private set; }

        private CharacterPhysicMoving _characterPhysicMoving; 


        public CharacterMovingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public void ActivateMoving(bool isActive)
        {
            _characterPhysicMoving.ActivateMoving(isActive);
        }

        public override void Disable()
        {
            _characterPhysicMoving.Disable();

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            _characterPhysicMoving = CharacterController.VisualBodyController.CharacterInScene.AddComponent<CharacterPhysicMoving>();
            _characterPhysicMoving.Init(this);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }
    }
}

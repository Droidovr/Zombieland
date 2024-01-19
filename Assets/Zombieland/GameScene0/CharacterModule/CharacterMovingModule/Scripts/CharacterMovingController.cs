using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterMovingController : Controller, ICharacterMovingController
    {
        public float RealMovingSpeed => _characterPhysicMoving.RealMovingSpeed;
        public ICharacterController CharacterController { get; private set; }

        private CharacterPhysicMoving _characterPhysicMoving;


        #region PUBLIC
        public CharacterMovingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public override void Disable()
        {
            _characterPhysicMoving.Disable();

            base.Disable();
        }
        #endregion PUBLIC


        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            GameObject character = CharacterController.VisualBodyController.CharacterInScene;
            character.AddComponent<CharacterPhysicMoving>();
            _characterPhysicMoving = character.GetComponent<CharacterPhysicMoving>();
            _characterPhysicMoving.Initialize(this);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
        #endregion PROTECTED
    }
}

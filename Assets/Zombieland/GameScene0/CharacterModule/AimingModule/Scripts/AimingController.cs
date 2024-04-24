using UnityEngine;
using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.AimingModule
{
    public class AimingController : Controller, IAimingController
    {
        public ICharacterController CharacterController { get; private set; }

        public AimingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public Transform GetTarget()
        {
            return default;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller does not have helpers scripts.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
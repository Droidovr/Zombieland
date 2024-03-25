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

        public Vector3 GetTarget()
        {
            return new Vector3(0f, 0f, 0f);
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
using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.NPCModule.NPCWeaponModule
{
    public class NPCWeaponController : Controller, INPCWeaponController
    {
        public event Action<Weapon> OnShotPerformed;
        public event Action OnShotFailed;

        public INPCController NPCController { get; private set; }
        public IWeapon Weapon { get; private set; }
        public Transform WeaponPointFire { get; private set; }

        public NPCWeaponController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }


        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
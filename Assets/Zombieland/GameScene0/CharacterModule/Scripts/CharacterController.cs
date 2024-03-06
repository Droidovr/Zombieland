using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.AnimationModule;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.CharacterModule.CharacterMovingModule;
using Zombieland.GameScene0.CharacterModule.EquipmentModule;
using Zombieland.GameScene0.CharacterModule.SensorModule;
using Zombieland.GameScene0.CharacterModule.TakeImpactModule;
using Zombieland.GameScene0.CharacterModule.WeaponModule;
using Zombieland.GameScene0.ImpactModule;
using Zombieland.GameScene0.RootModule;
using Zombieland.GameScene0.UIModule;
using Zombieland.GameScene0.VisualBodyModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public class CharacterController : Controller, ICharacterController, IImpactable
    {
        public IRootController RootController { get; private set; }
        public ICharacterDataController CharacterDataController { get; private set; }
        public IWeaponController WeaponController { get; private set; }
        public IVisualBodyController VisualBodyController { get; private set; }
        public ICharacterMovingController CharacterMovingController { get; private set; }
        public ISensorController SensorController { get; private set; }
        public ITakeImpactController TakeImpactController { get; private set;}
        public IEquipmentController EquipmentController { get; private set;}
        public IAnimationController AnimationController { get; private set; }
        public IBuffDebuffController BuffDebuffController { get; private set; }

        public Transform CharacterTransform => VisualBodyController.CharacterInScene.transform;

        private readonly IRootController _rootController;

        public CharacterController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RootController = parentController as IRootController;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller does not have helpers scripts.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            CharacterDataController = new CharacterDataController(this, new List<IController> { (IController)RootController.GameDataController });
            subsystemsControllers.Add((IController)CharacterDataController);

            WeaponController = new WeaponController(this, new List<IController> { (IController)CharacterDataController, (IController)EquipmentController, (IController)RootController.UIController });
            subsystemsControllers.Add((IController)WeaponController);

            VisualBodyController = new VisualBodyController(this, new List<IController> { (IController)RootController.EnvironmentController });
            subsystemsControllers.Add((IController)VisualBodyController);

            CharacterMovingController = new CharacterMovingController(this, new List<IController> { (IController) RootController.UIController, (IController) CharacterDataController, (IController) VisualBodyController, (IController) AnimationController });
            subsystemsControllers.Add((IController)CharacterMovingController);

            SensorController = new SensorController(this, new List<IController>{(IController)VisualBodyController});
            subsystemsControllers.Add((IController)SensorController);
            
            TakeImpactController = new TakeImpactController(this, null);
            subsystemsControllers.Add((IController)TakeImpactController);
            
            EquipmentController = new EquipmentController(this, new List<IController>{(IController)CharacterDataController});
            subsystemsControllers.Add((IController)EquipmentController);

            AnimationController = new AnimationController(this, new List<IController>{(IController)CharacterMovingController});
            subsystemsControllers.Add ((IController)AnimationController);

            BuffDebuffController = new BuffDebuffController(this, null);
            subsystemsControllers.Add((IController)BuffDebuffController);

            //WeaponController = new WeaponController(this, new List<IController> { (IController)CharacterDataController, (IController)EquipmentController, (IController)RootController.UIController });
            //subsystemsControllers.Add((IController)WeaponController);
        }
        
        public void ApplyImpact(IImpactController impactController)
        {
            // if its direct damage
            TakeImpactController.ProcessImpact(impactController);
        }
    }
}
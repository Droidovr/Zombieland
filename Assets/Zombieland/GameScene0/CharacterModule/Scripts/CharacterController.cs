using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.CharacterModule.EquipmentModule;
using Zombieland.GameScene0.CharacterModule.SensorModule;
using Zombieland.GameScene0.CharacterModule.TakeDamageModule;
using Zombieland.GameScene0.CharacterModule.TakeDamageModule.Scripts;
using Zombieland.GameScene0.CharacterModule.WeaponModule;
using Zombieland.GameScene0.ImpactModule;
using Zombieland.GameScene0.RootModule;
using Zombieland.GameScene0.VisualBodyModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public class CharacterController : Controller, ICharacterController, IImpactable
    {
        public ICharacterDataController CharacterDataController { get; private set; }
        public IWeaponController WeaponController { get; private set; }
        public IVisualBodyController VisualBodyController { get; private set; }
        public ISensorController SensorController { get; private set; }
        public ITakeDamageController TakeDamageController { get; private set;}
        public IEquipmentController EquipmentController { get; private set;}

        private readonly IRootController _rootController;

        public CharacterController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            _rootController = parentController as IRootController;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller does not have helpers scripts.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            CharacterDataController = new CharacterDataController(this, new List<IController>{(IController)_rootController.GameDataController});
            subsystemsControllers.Add((IController)CharacterDataController);

            WeaponController = new WeaponController(this, new List<IController>{(IController)CharacterDataController});
            subsystemsControllers.Add((IController)WeaponController);

            VisualBodyController = new VisualBodyController(this, new List<IController> { (IController)_rootController.EnvironmentController });
            subsystemsControllers.Add((IController)VisualBodyController);

            SensorController = new SensorController(this, new List<IController>{(IController)VisualBodyController});
            subsystemsControllers.Add((IController)SensorController);
            
            TakeDamageController = new TakeDamageController(this, null);
            subsystemsControllers.Add((IController)TakeDamageController);
            
            EquipmentController = new EquipmentController(this, new List<IController>{(IController)CharacterDataController});
            subsystemsControllers.Add((IController)EquipmentController);
        }
        
        public void ApplyImpact(IImpactController impactController)
        {
            // if its direct damage
            TakeDamageController.ProcessDamage(impactController);
            
            // if its buffs/debuffs
            // BuffDebuffController.ProcessEffect
        }
    }
}

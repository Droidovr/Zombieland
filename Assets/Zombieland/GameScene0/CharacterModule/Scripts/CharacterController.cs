using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.CharacterModule.CharacterMovingModule;
using Zombieland.GameScene0.CharacterModule.WeaponModule;
using Zombieland.GameScene0.RootModule;
using Zombieland.GameScene0.VisualBodyModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public class CharacterController : Controller, ICharacterController
    {
        public ICharacterDataController CharacterDataController { get; private set; }
        public IWeaponController WeaponController { get; private set; }
        public IVisualBodyController VisualBodyController { get; private set; }
        public ICharacterMovingController CharacterMovingController { get; private set; }


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
            CharacterDataController = new CharacterDataController(this, new List<IController> { (IController)_rootController.GameDataController });
            subsystemsControllers.Add((IController)CharacterDataController);

            WeaponController = new WeaponController(this, new List<IController> { (IController)CharacterDataController });
            subsystemsControllers.Add((IController)WeaponController);

            VisualBodyController = new VisualBodyController(this, new List<IController> { (IController)_rootController.EnvironmentController });
            subsystemsControllers.Add((IController)VisualBodyController);

            CharacterMovingController = new CharacterMovingController(this, new List<IController> 
                                                                            {
                                                                                (IController) _rootController.UIController,
                                                                                (IController) CharacterDataController,
                                                                                (IController) VisualBodyController 
                                                                            });
            subsystemsControllers.Add((IController)CharacterMovingController);
        }
    }
}
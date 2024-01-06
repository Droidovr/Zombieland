using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.EnvironmentModule;
using Zombieland.GameScene0.GameDataModule;

namespace Zombieland.GameScene0.RootModule
{
    public class RootController : Controller, IRootController
    {
        public ICharacterController CharacterController { get; private set; }
        public IGameDataController GameDataController { get; private set; }
        public IEnvironmentController EnvironmentController { get; private set; }


        public RootController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }
        
        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            GameDataController = new GameDataController(this, null);
            subsystemsControllers.Add((IController)GameDataController);
            
            EnvironmentController = new EnvironmentController(this, new List<IController> {(IController) GameDataController});
            subsystemsControllers.Add((IController)EnvironmentController);
            
            CharacterController = new CharacterController(this, new List<IController> {(IController) EnvironmentController, (IController) GameDataController});
            subsystemsControllers.Add((IController)CharacterController);
        }
    }
}
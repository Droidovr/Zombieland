using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.GameDataModule;

namespace Zombieland.GameScene0.RootModule
{
    public class RootController : Controller, IRootController
    {
        public ICharacterController CharacterController { get; private set; }
        public IGameDataController GameDataController { get; private set; }
        
        
        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            GameDataController = new GameDataController(this);
            subsystemsControllers.Add((IController)GameDataController);

            CharacterController = new CharacterController(this);
            subsystemsControllers.Add((IController)CharacterController);
        }
    }
}
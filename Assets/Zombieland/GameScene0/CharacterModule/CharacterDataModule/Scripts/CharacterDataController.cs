using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule;


namespace Zombieland.CharacterModule.CharacterDataModule
{
    public class CharacterDataController : Controller, ICharacterDataController
    {
        public ICharacterController CharacterController { get; }

        
        public CharacterDataController(IController parentController)
        {
            CharacterController = (ICharacterController)parentController;
        }
        
        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            
        }
    }
}
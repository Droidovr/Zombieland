using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class TestCharacterDataController : Controller, ITestCharacterDataController
    {
        public ICharacterController CharacterController { get; }

        private TestCharacterData _testCharacterData;

        public TestCharacterDataController(IController parentController) 
        {
            //CharacterController = (ICharacterController)parentController;
        }

        public TestCharacterData GetCharacterData()
        {
            return _testCharacterData;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            _testCharacterData = new TestCharacterData();
        }
    }
}

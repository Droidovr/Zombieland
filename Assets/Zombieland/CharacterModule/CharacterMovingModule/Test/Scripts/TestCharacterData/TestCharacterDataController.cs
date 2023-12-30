using System;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class TestCharacterDataController : IController, ITestCharacterDataController
    {
        public bool IsActive { get; private set; }
        public event Action<string, IController> OnReady;

        private TestCharacterData _testCharacterData;

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Initialize<T>(T parentController)
        {
            _testCharacterData = new TestCharacterData();

            if (_testCharacterData != null)
            { 
                IsActive = true;
            }
            OnReady?.Invoke(String.Empty, this);
        }
        public TestCharacterData GetCharacterData()
        {
            return _testCharacterData;
        }
    }
}

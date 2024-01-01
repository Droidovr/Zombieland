using System;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class TestCharacterDataController : IController, ITestCharacterDataController
    {
        public bool IsActive { get; private set; }
        public event Action<string, IController> OnReady;

        private TestCharacterData _testCharacterData;

        #region PUBLIC
        public void Disable()
        {
            SetSystemsActivity(false);
        }

        public void Initialize<T>(T parentController)
        {
            _testCharacterData = new TestCharacterData();

            SetSystemsActivity(true);
        }
        public TestCharacterData GetCharacterData()
        {
            return _testCharacterData;
        }
        #endregion PUBLIC


        #region PRIVATE
        private void SetSystemsActivity(bool isActive)
        {
            IsActive = isActive;
            OnReady?.Invoke(String.Empty, this);
        }
        #endregion PRIVATE
    }
}

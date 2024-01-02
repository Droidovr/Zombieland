using System;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule
{
    public class CharacterController : IController, ICharacterController
    {
        public bool IsReady { get; private set; }
        public event Action<string, IController> OnReady;
        
        private IRootController _rootController;
        
        
        public void Initialize<T>(T parentController)
        {
            _rootController = parentController as IRootController;
            CreateSubsystems();
        }

        public void Disable()
        {
            IsReady = false;
            OnReady?.Invoke(String.Empty, this);
        }
        
        
        private void CreateSubsystems()
        {
            throw new NotImplementedException();
        }
    }
}

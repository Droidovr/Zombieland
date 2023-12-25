using System;
using Zombieland.RootModule;

namespace Zombieland.GameDataModule
{
    public class GameDataController : IController, IGameDataController
    {
        public bool IsActive { get; private set; }
        public event Action<string, IController> OnReady;

        private IRootController _rootController;
        private IStorage _storage;

        
        public void Initialize<T>(T parentController)
        {
            _rootController = parentController as IRootController;
            CreateSubsystems();
        }
        
        public void Disable()
        {
            IsActive = false;
            OnReady?.Invoke(String.Empty, this);
        }

        public void SaveDada<T>(string name, T data)
        {
            _storage.SaveDada(name, data);
        }

        public T GetData<T>(string name)
        {
            return _storage.GetData<T>(name);
        }

        
        private void CreateSubsystems()
        {
#if UNITY_EDITOR
            _storage = new ResourcesStorage();
#else
            _storage = new PlayerPrefsStorage();
#endif
            IsActive = true;
            OnReady?.Invoke(String.Empty, this);
        }
    }
}

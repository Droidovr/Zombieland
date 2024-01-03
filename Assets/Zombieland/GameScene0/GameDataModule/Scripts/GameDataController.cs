
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.GameDataModule
{
    public class GameDataController : Controller, IGameDataController
    {
        private IStorage _storage;
        public IRootController RootController { get; }


        public GameDataController(IController parentController)
        {
            RootController = parentController as IRootController;
        }

        public void SaveDada<T>(string name, T data)
        {
            _storage.SaveDada(name, data);
        }

        public T GetData<T>(string name)
        {
            return _storage.GetData<T>(name);
        }
        

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
#if UNITY_EDITOR
            _storage = new ResourcesStorage();
#else
            _storage = new PlayerPrefsStorage();
#endif
        }
    }
}

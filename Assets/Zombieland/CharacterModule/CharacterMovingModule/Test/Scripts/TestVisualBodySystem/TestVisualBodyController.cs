using System;
using UnityEngine;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class TestVisualBodyController : MonoBehaviour, IController, ITestVisualBodyController
    {
        public bool IsActive { get; private set; }
        public event Action<string, IController> OnReady;

        private GameObject _characterOnScene;

        public void Initialize<T>(T parentController)
        {
            _characterOnScene = GameObject.Find("Character");

            if (_characterOnScene != null)
            {
                IsActive = true;
            }
            OnReady?.Invoke(String.Empty, this);
        }

        public GameObject GetCharacter()
        {
            return _characterOnScene;
        }

        public void Disable()
        {
            throw new NotImplementedException();
        }
    }
}
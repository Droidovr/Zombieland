using System;
using UnityEngine;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class TestVisualBodyController : MonoBehaviour, IController, ITestVisualBodyController
    {
        public bool IsActive { get; private set; }
        public event Action<string, IController> OnReady;

        private GameObject _prefab;
        private string _prefabName = "Character";
        private Vector3 _positionInstantiatePrefab = new Vector3(0, 1f, 0);
        private GameObject _characterGameObject;

        public void Initialize<T>(T parentController)
        {
            _prefab = Resources.Load<GameObject>(_prefabName);
            
            _characterGameObject = Instantiate(_prefab, _positionInstantiatePrefab, Quaternion.identity);

            if (_characterGameObject != null)
            {
                IsActive = true;
            }
            OnReady?.Invoke(String.Empty, this);
        }

        public GameObject GetCharacterGameobject()
        {
            return _characterGameObject;
        }

        public void Disable()
        {
            throw new NotImplementedException();
        }
    }
}
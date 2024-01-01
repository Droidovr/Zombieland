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

        public void Disable()
        {
            SetSystemsActivity(false);
        }

        public void Initialize<T>(T parentController)
        {
            _prefab = Resources.Load<GameObject>(_prefabName);
            _characterGameObject = Instantiate(_prefab, _positionInstantiatePrefab, Quaternion.identity);

            Camera.main.GetComponent<MovingCamera>().Character = _characterGameObject;

            if (_characterGameObject != null)
            {
                SetSystemsActivity(true);
            }
        }

        public GameObject GetCharacterGameobject()
        {
            return _characterGameObject;
        }

        private void SetSystemsActivity(bool isActive)
        {
            IsActive = isActive;
            OnReady?.Invoke(String.Empty, this);
        }
    }
}
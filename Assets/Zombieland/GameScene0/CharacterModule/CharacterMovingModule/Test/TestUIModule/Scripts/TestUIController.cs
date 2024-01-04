using System;
using UnityEngine;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class TestUIController : MonoBehaviour, IController, ITestUIController
    {
        public bool IsActive { get; private set; }

        public event Action<string, IController> OnReady;
        public event Action<Vector2> OnJoustickMoved;

        private GameObject _prefab;
        private string _prefabName = "InputSystem";
        private GameObject _inputSystemGameObject;
        private InputSystem _inputSystem;

        #region PUBLIC
        public void Disable()
        {
            SetSystemsActivity(false);
        }

        public void Initialize<T>(T parentController)
        {
            _prefab = Resources.Load<GameObject>(_prefabName);
            _inputSystemGameObject = Instantiate(_prefab);

            Canvas canvasComponent = FindObjectOfType<Canvas>();
            _inputSystemGameObject.transform.SetParent(canvasComponent.transform);

            _inputSystem = _inputSystemGameObject.GetComponent<InputSystem>();
            _inputSystem.OnJoustickMoved += HandleJoustickMoved;

            SetSystemsActivity(true);
        }
        #endregion PUBLIC

        #region PRIVATE
        private void HandleJoustickMoved(Vector2 joystickPosition)
        {
            OnJoustickMoved?.Invoke(joystickPosition);
        }

        private void SetSystemsActivity(bool isActive)
        {
            IsActive = isActive;
            OnReady?.Invoke(String.Empty, this);
        }
        #endregion PRIVATE
    }
}

using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CreateInputGameobject : MonoBehaviour
    {
        public InputSystem InputSystem => _inputSystem;

        private GameObject _prefab;
        private string _prefabName = "InputSystem";
        private GameObject _inputSystemGameObject;
        private InputSystem _inputSystem;

        public void Init()
        {
            _prefab = Resources.Load<GameObject>(_prefabName);
            _inputSystemGameObject = Instantiate(_prefab);

            Canvas canvasComponent = FindObjectOfType<Canvas>();
            _inputSystemGameObject.transform.SetParent(canvasComponent.transform);

            _inputSystem = _inputSystemGameObject.GetComponent<InputSystem>();
        }
    }
}

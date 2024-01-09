using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class CreateInputSystem : MonoBehaviour
    {
        public InputSystem InputSystem => _inputSystem;

        private string _prefabName = "InputSystem";
        private InputSystem _inputSystem;

        public void Init()
        {
            GameObject _prefab = Resources.Load<GameObject>(_prefabName);

            Canvas canvas = GameObject.FindWithTag("YourCanvasTag").GetComponent<Canvas>();

            GameObject _inputSystemGameobject = Instantiate(_prefab, canvas.transform);

            _inputSystem = _inputSystemGameobject.GetComponent<InputSystem>();
        }
    }
}

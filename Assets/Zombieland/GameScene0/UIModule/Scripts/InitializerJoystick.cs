using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class InitializerJoystick : MonoBehaviour
    {
        public InputSystem InputSystem => _inputSystem;

        private string _prefabName = "Joystick";
        private InputSystem _inputSystem;

        public void Init()
        {
            GameObject _prefab = Resources.Load<GameObject>(_prefabName);

            Canvas canvas = GameObject.FindWithTag("InputSystem").GetComponent<Canvas>();

            GameObject _inputSystemGameobject = Instantiate(_prefab, canvas.transform);

            _inputSystem = _inputSystemGameobject.GetComponent<InputSystem>();
        }
    }
}

using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class InitializerJoystick
    {
        public InputManager InputManager { get; private set; }

        private const string JOYSTICK_PREFAB_NAME = "Input";        

        public void Init()
        {
            var _prefab = Resources.Load<GameObject>(JOYSTICK_PREFAB_NAME);

            Canvas canvas = GameObject.FindWithTag("InputSystem").GetComponent<Canvas>();

            GameObject _inputSystemGameobject = GameObject.Instantiate(_prefab, canvas.transform);

            InputManager = _inputSystemGameobject.GetComponent<InputManager>();
            //InputManager = new InputManager();
        }
    }
}

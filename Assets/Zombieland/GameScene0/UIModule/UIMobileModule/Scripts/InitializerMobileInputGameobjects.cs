using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class InitializerMobileInputGameobjects
    {
        public InputMobileManager InputMobileManager { get; private set; }

        private const string JOYSTICK_PREFAB_NAME = "Input";        

        public void Init()
        {
            var _prefab = Resources.Load<GameObject>(JOYSTICK_PREFAB_NAME);

            Canvas canvas = GameObject.FindWithTag("Input").GetComponent<Canvas>();

            GameObject _inputSystemGameobject = GameObject.Instantiate(_prefab, canvas.transform);

            InputMobileManager = _inputSystemGameobject.GetComponent<InputMobileManager>();
        }
    }
}

using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class InitializerMobileInputGameobjects
    {
        public InputMobile InputMobile { get; private set; }

        private const string JOYSTICK_PREFAB_NAME = "Input";        

        public void Init()
        {
            var _prefab = Resources.Load<GameObject>(JOYSTICK_PREFAB_NAME);

            Canvas canvas = GameObject.FindWithTag("Input").GetComponent<Canvas>();

            GameObject _inputSystemGameobject = GameObject.Instantiate(_prefab, canvas.transform);

            InputMobile = _inputSystemGameobject.GetComponent<InputMobile>();
        }
    }
}

using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class InitializerMobileInputGameobjects
    {
        public InputMobile InputMobile { get; private set; }

        private const string JOYSTICK_PREFAB_NAME = "Input";        

        public void Init()
        {
            GameObject prefab = Resources.Load<GameObject>(JOYSTICK_PREFAB_NAME);

            Canvas canvas = GameObject.FindWithTag("Input").GetComponent<Canvas>();

            GameObject inputSystemGameobject = GameObject.Instantiate(prefab, canvas.transform);

            InputMobile = inputSystemGameobject.GetComponent<InputMobile>();
        }
    }
}
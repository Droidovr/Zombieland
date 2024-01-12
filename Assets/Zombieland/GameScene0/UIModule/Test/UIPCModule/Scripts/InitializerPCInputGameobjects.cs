using UnityEngine;
using UnityEngine.InputSystem;

namespace Zombieland.GameScene0.UIModule
{
    public class InitializerPCInputGameobjects
    {
        public InputPCManager InputPCManager { get; private set; }

        public void Init()
        {
            GameObject _inputSystemGameobject = new GameObject();

            _inputSystemGameobject.AddComponent<PlayerInput>();
            _inputSystemGameobject.AddComponent<InputPCManager>();

            InputPCManager = _inputSystemGameobject.GetComponent<InputPCManager>();
        }
    }
}

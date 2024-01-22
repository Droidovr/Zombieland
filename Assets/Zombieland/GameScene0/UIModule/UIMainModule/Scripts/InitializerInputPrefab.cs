using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Zombieland.GameScene0.UIModule
{
    public class InitializerInputPrefab
    {
        public Input Input { get; private set; }

        private const string INPUT_MOBILE_PREFAB_NAME = "MainUICanvas";
        private const string INPUT_PC_PREFAB_NAME = "InputPC";

        public void Init()
        {
            GameObject eventSystem = new GameObject();
            eventSystem.name = "EventSystem";
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<InputSystemUIInputModule>();

//#if UNITY_STANDALONE || UNITY_EDITOR
//            GameObject prefab = Resources.Load<GameObject>(INPUT_PC_PREFAB_NAME);
//            GameObject inputSystemGameobject = GameObject.Instantiate(prefab);
//#else
            GameObject prefab = Resources.Load<GameObject>(INPUT_MOBILE_PREFAB_NAME);
            GameObject inputSystemGameobject = GameObject.Instantiate(prefab);
//#endif
            Input = inputSystemGameobject.GetComponentInChildren<Input>();
        }
    }
}
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class InitializerPCInputGameobjects
    {
        public InputPC InputPC { get; private set; }

        private const string INPUT_PREFAB_NAME = "InputPC";

        public void Init()
        {
            GameObject prefab = Resources.Load<GameObject>(INPUT_PREFAB_NAME);

            GameObject inputSystemGameobject = GameObject.Instantiate(prefab);

            InputPC = inputSystemGameobject.GetComponent<InputPC>();
        }
    }
}

using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class TestInitializerEnvironment
    {
        private const string ENVIRONMENT_PREFAB_NAME = "Level";

        public void Init()
        {
            GameObject prefab = Resources.Load<GameObject>(ENVIRONMENT_PREFAB_NAME);
            Debug.Log(prefab == null);
            GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}


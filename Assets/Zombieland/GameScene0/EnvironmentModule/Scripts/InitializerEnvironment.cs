using UnityEngine;

namespace Zombieland.GameScene0.EnvironmentModule
{
    public class InitializerEnvironment
    {
        private const string ENVIRONMENT_PREFAB_NAME = "Level0";

        public void Init()
        {
            GameObject prefab = Resources.Load<GameObject>(ENVIRONMENT_PREFAB_NAME);
            GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}


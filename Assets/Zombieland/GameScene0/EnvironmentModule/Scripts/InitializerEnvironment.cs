using UnityEngine;

namespace Zombieland.GameScene0.EnvironmentModule
{
    public class InitializerEnvironment
    {
        public void Init(EnvironmentData environmentData)
        {
            GameObject prefab = Resources.Load<GameObject>(environmentData.CurrentLevelName);
            GameObject.Instantiate(prefab);
        }
    }
}


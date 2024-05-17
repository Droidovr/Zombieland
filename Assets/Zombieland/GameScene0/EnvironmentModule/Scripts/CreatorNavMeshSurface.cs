using UnityEngine;

namespace Zombieland.GameScene0.EnvironmentModule
{
    public class CreatorNavMeshSurface
    {
        private const string PREFAB_NAME = "NavMeshSurfaceLevel1";

        private GameObject _gameObjectNavMeshSurface;

        public CreatorNavMeshSurface()
        {
            GameObject prefab = Resources.Load<GameObject>(PREFAB_NAME);

            _gameObjectNavMeshSurface = GameObject.Instantiate(prefab);
        }

        public void Destroy()
        {
            GameObject.Destroy(_gameObjectNavMeshSurface);
        }
    }
}
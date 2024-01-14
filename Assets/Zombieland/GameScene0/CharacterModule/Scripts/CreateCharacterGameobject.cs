using UnityEngine;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class CreateCharacterGameobject
    {
        private const string CHARACTER_PREFAB_NAME = "PoliceWoman";

        public GameObject CreateCharacter(Vector3 spawnPosition)
        {
            GameObject prefab = Resources.Load<GameObject>(CHARACTER_PREFAB_NAME);

            return GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}
using UnityEngine;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class CreateCharacterPrefab
    {
        private const string CHARACTER_PREFAB_NAME = "Characters/Character0";

        public GameObject CreateCharacter(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            GameObject prefab = Resources.Load<GameObject>(CHARACTER_PREFAB_NAME);

            return GameObject.Instantiate(prefab, spawnPosition, spawnRotation);
        }
    }
}
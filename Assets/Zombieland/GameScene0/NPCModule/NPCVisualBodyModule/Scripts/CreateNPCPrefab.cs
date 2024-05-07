using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class CreateNPCPrefab
    {
        private const string CHARACTER_PREFAB_NAME = "NPC_Prefab_0";

        public GameObject CreateNPC(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            GameObject prefab = Resources.Load<GameObject>(CHARACTER_PREFAB_NAME);

            return GameObject.Instantiate(prefab, spawnPosition, spawnRotation);
        }

        public void Destroy(GameObject characterInScene)
        {
            GameObject.Destroy(characterInScene);
        }
    }
}
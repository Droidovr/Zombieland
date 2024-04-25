using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterVFX
{
    public class VFXCreator
    {
        private ICharacterVFXController _characterVFXController;

        public VFXCreator(ICharacterVFXController characterVFXController) 
        {
            _characterVFXController = characterVFXController;
            
        }

        public GameObject CtreateVFX(string nameVFX, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            GameObject prefab = Resources.Load<GameObject>(nameVFX);

            return GameObject.Instantiate(prefab, spawnPosition, spawnRotation);
        }
    }
}
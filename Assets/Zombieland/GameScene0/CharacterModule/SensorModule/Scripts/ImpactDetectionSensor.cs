using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SensorModule
{
    public class ImpactDetectionSensor : MonoBehaviour
    {
        private IImpactable _characterController;

        public void Init(IImpactable characterController)
        {
            _characterController = characterController;
        }

       public IImpactable GetDamageableObject()
       {
           return _characterController;
       }
    }
}
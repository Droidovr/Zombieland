using UnityEngine;
using Zombieland.GameScene0.ImpactModule;

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
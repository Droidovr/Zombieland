using UnityEngine;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.SensorModule
{
    public class ImpactDetectionSensor : MonoBehaviour, IImpactable
    {
        public Transform ImpactObjectTransform => transform;
        private IImpactable _characterController;

        public void Init(IImpactable characterController)
        {
            _characterController = characterController;
        }

        public void ApplyImpact(IImpactController impactController)
       {

       }
    }
}
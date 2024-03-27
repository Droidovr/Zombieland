using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule
{
    public class Impactable : MonoBehaviour, IImpactable
    {
        public ICharacterController Owner { get; set; }
    }
}
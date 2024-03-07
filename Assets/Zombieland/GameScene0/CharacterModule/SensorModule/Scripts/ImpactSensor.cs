using UnityEngine;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.SensorModule
{
    public class ImpactSensor : MonoBehaviour, IImpactable
    {
        public ICharacterController Owner { get; set; }
        public Transform Transform => transform;
    }
}
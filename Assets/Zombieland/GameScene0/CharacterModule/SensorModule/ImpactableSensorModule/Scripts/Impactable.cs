using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule
{
    public class Impactable : MonoBehaviour, IImpactable
    {
        public IController Controller { get; private set; }

        public void Init(IController controller)
        {
            Controller = controller;
        }
    }
}
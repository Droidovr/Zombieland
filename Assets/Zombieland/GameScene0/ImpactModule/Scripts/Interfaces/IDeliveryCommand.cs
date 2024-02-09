using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public interface IDeliveryCommand : IImpactCommand
    {
        public GameObject ImpactObject { get; set; }
        public void ApplyImpactOnDelivery();
        public void Deactivate();
    }
}
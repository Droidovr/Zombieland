using System;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ImpactData
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public IDeliveryCommand DeliveryHandler { get; set; }
    }
}

using System;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ImpactData
    {
        public string Name;
        public string ID;
        public IImpactCommand DeliveryHandler;
    }
}

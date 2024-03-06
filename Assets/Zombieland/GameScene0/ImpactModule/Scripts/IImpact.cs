using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpact
    {
        public IController ImpactOwner { get; set; }
        public IImpactCommand Assembler { get; set; }
        public IImpactCommand Delivery { get; set; }
        public IImpactCommand DirectImpact { get; set; }
        public IImpactCommand BuffDebuffInjection { get; set; }
        public void Activate();
        public void Deactivate();
    }
}
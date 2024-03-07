using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;

namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpact
    {
        public ICharacterController ImpactOwner { get; set; }
        public List<IImpactable> Targets { get; set; }
        public GameObject ImpactObject { get; set; }
        public IImpactCommand Assembler { get; set; }
        public IImpactCommand Delivery { get; set; }
        public IImpactCommand DirectImpact { get; set; }
        public IImpactCommand BuffDebuffInjection { get; set; }
        public void Activate();
        public void Deactivate();
    }
}
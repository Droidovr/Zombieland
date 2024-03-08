using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class Impact : IImpact
    {
        [JsonIgnore] public ICharacterController ImpactOwner { get; set; }
        [JsonIgnore] public List<IImpactable> Targets { get; set; }
        [JsonIgnore] public GameObject ImpactObject { get; set; }
        public IImpactCommand Assembler { get; set; }
        public IImpactCommand Delivery { get; set; }
        public IImpactCommand InitialImpact { get; set; }
        public IImpactCommand BuffDebuffInjection { get; set; }

        public void Activate()
        {
            Assembler.Impact = this;
            Delivery.Impact = this;
            InitialImpact.Impact = this;
            BuffDebuffInjection.Impact = this;
            Assembler.Execute();
        }

        public void Deactivate()
        {
            Assembler.Deactivate();
            Delivery.Deactivate();
            InitialImpact.Deactivate();
            BuffDebuffInjection.Deactivate();
        }
    }
}

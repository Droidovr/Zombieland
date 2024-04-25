using System;
using System.Linq;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class Impact : IImpact
    {
        public ImpactData ImpactData { get; set; }
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
            ApplyBuffsDebuffsToImpact();
            Assembler.Execute();
        }

        public void Deactivate()
        {
            Assembler.Deactivate();
            Delivery.Deactivate();
            InitialImpact.Deactivate();
            BuffDebuffInjection.Deactivate();
        }

        private void ApplyBuffsDebuffsToImpact()
        {
            if (ImpactData.ImpactOwner.BuffDebuffController.CountBuffDebuff <= 0) return;
            
            var initialImpact = (IInitialImpactCommand)InitialImpact;
            var updatedInitialImpactList = initialImpact.InitialImpactData
                .Select(impact => ImpactData.ImpactOwner.BuffDebuffController.GetProcessedImpactValue(impact)).
                ToList();
            initialImpact.InitialImpactData = updatedInitialImpactList;

            var buffDebuffInjection = (BuffDebuffInjection)BuffDebuffInjection;
            foreach (var buff in buffDebuffInjection.Buffs)
            {
                buff.BuffDebuffData.DirectImpactData = ImpactData.ImpactOwner.BuffDebuffController.GetProcessedImpactValue(buff.BuffDebuffData.DirectImpactData);
            }
            
            foreach (var debuff in buffDebuffInjection.Debuffs)
            {
                debuff.BuffDebuffData.DirectImpactData = ImpactData.ImpactOwner.BuffDebuffController.GetProcessedImpactValue(debuff.BuffDebuffData.DirectImpactData);
            }
        }
    }
}
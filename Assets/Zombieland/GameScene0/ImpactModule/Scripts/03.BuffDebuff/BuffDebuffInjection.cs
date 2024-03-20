using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class BuffDebuffInjection : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public List<IBuffDebuffCommand> Buffs { get; set; }
        public List<IBuffDebuffCommand> Debuffs { get; set; }

        public void Execute()
        {
            foreach (var target in Impact.ImpactData.Targets)
            {
                if (Buffs!= null && Buffs.Count > 0)
                {
                    foreach (var buff in Buffs)
                    {
                        buff.BuffDebuffData.Owner = Impact.ImpactData.ImpactOwner;
                        buff.BuffDebuffData.ImpactTarget = target.Owner;
                    }
                    target.TestApplyBuffs(Buffs);
                    //target.Owner.BuffDebuffController.InjectBuffs(Buffs);
                }

                if (Debuffs!= null && Debuffs.Count > 0)
                {
                    foreach (var debuff in Debuffs)
                    {
                        debuff.BuffDebuffData.Owner = Impact.ImpactData.ImpactOwner;
                        debuff.BuffDebuffData.ImpactTarget = target.Owner;
                    }
                    target.TestApplyDebuffs(Debuffs);
                    //target.Owner.BuffDebuffController.InjectDebuffs(Debuffs);
                }
            }
            
            Impact.Deactivate();
        }

        public void Deactivate()
        {
            // Has no implementation
        }
    }
}

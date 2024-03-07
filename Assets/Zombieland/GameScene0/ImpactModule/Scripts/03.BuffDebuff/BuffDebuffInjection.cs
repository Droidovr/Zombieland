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
            foreach (var target in Impact.Targets)
            {
                if (Buffs.Count > 0)
                {
                    foreach (var buff in Buffs)
                    {
                        buff.BuffDebuffData.Owner = Impact.ImpactOwner;
                        buff.BuffDebuffData.ImpactTarget = target.Owner;
                    }
                    target.Owner.BuffDebuffController.InjectBuffs(Buffs);
                }

                if (Debuffs.Count > 0)
                {
                    foreach (var debuff in Debuffs)
                    {
                        debuff.BuffDebuffData.Owner = Impact.ImpactOwner;
                        debuff.BuffDebuffData.ImpactTarget = target.Owner;
                    }
                    target.Owner.BuffDebuffController.InjectDebuffs(Buffs);
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

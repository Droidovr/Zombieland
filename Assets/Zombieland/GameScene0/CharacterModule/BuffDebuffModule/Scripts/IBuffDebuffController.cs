using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public interface IBuffDebuffController
    {
        void InjectBuffs(List<IBuffDebuffCommand> buffs);

        void InjectDebuffs(List<IBuffDebuffCommand> debuffs);

        SingleImpact GetProcessedImpactValue(SingleImpact buffDebuff);
    }
}
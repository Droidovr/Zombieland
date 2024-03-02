using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public interface IBuffDebuffController
    {
        Dictionary<string, IBuffDebuffCommand> Buffs { get; set; }
        Dictionary<string, IBuffDebuffCommand> Debuffs { get; set; }
        ICharacterController CharacterController { get; }
    

    void InjectBuffs(List<IBuffDebuffCommand> buffs);

        void InjectDebuffs(List<IBuffDebuffCommand> debuffs);

        SingleImpact GetProcessedImpactValue(SingleImpact buffDebuff);
    }
}
using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public interface IBuffDebuffController
    {
        void InjectBuffs(List<ICommand> buffs);
        void InjectDebuffs(List<ICommand> debuffs);
    }
}
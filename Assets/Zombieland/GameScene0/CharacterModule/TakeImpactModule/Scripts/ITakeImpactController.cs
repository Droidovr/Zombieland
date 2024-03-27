using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.CharacterModule.TakeImpactModule
{
    public interface ITakeImpactController
    {
        ICharacterController CharacterController { get; }
        void ApplyImpact(List<DirectImpactData> damageTakens);
    }
}
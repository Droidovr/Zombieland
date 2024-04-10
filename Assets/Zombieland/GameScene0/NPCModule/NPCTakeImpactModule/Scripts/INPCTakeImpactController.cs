using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.NPCModule.NPCTakeImpactModule
{
    public interface INPCTakeImpactController
    {
        void ApplyImpact(List<DirectImpactData> impact);
    }
}

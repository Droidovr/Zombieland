using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.NPCModule.NPCTakeImpactModule
{
    public interface INpcTakeImpactController
    {
        public void ApplyImpact(List<DirectImpactData> impact);
    }
}

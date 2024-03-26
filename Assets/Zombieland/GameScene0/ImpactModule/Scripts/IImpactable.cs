using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpactable
    {
        public ICharacterController Owner { get; set; }
        public Transform Transform { get; }

        public void TestApplyDirectImpact(List<DirectImpactData> directImpactDataList);
        public void TestApplyBuffs(List<IBuffDebuffCommand> buffs);
        public void TestApplyDebuffs(List<IBuffDebuffCommand> debuffs);
    }
}
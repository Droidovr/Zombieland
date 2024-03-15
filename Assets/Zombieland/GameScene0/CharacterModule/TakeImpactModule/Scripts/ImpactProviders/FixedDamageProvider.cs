using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;

namespace Assets.Zombieland.GameScene0.CharacterModule.TakeImpactModule.Scripts.ImpactProviders
{
    internal class FixedDamageProvider : IImpactProvider
    {
        private CharacterData _characterData;
        public FixedDamageProvider(CharacterData characterData) {
            _characterData = characterData;
        }
        public void provideImpact(DirectImpactData impactData)
        {
            _characterData.HP = Math.Max(0, _characterData.HP - impactData.AbsoluteValue);
        }
        

    }
}

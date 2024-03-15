using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;

namespace Assets.Zombieland.GameScene0.CharacterModule.TakeImpactModule.Scripts.ImpactProviders
{
    internal interface IImpactProvider
    {
        void provideImpact(DirectImpactData impactData);
      
    }
}

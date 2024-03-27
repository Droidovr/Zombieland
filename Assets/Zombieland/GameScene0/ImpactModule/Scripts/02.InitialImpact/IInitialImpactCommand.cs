using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.ImpactModule;

public interface IInitialImpactCommand : IImpactCommand
{
    public List <DirectImpactData> InitialImpactData { get; set; }
}

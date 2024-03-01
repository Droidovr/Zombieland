namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public interface IBuffDebuffCommand : ICommand
    {
        string Name { get; }
        ICharacterController ImpactTarget { get; }
        ICharacterController Owner { get; }

        SingleImpact GetProcessedImpactValue(SingleImpact buffDebuff);
    }
}
namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public interface IBuffDebuffCommand : ICommand
    {
        string Name { get; }
        SingleImpact SingleImpact { get; }
        IBuffDebuffController buffDebuffController { get; set; }
        ICharacterController ImpactTarget { get; set; }
        ICharacterController Owner { get; set; }

        void Destroy();

        SingleImpact GetProcessedImpactValue(SingleImpact buffDebuff);
    }
}
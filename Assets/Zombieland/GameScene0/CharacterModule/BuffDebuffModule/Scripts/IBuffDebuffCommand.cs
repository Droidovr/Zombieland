namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public interface IBuffDebuffCommand : ICommand
    {
        string Name { get; }
        DirectImpactSetting DirectImpactSetting { get; }
        ICharacterController ImpactTarget { get; set; }
        ICharacterController Owner { get; set; }

        void Destroy();

        DirectImpactSetting GetProcessedImpactValue(DirectImpactSetting buffDebuff);
    }
}
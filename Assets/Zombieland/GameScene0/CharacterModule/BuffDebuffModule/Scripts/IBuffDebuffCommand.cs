namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public interface IBuffDebuffCommand : ICommand
    {
        BuffDebuffData BuffDebuffData { get; set; }

        void Destroy();

        DirectImpactData GetProcessedImpactValue(DirectImpactData buffDebuff);
    }
}
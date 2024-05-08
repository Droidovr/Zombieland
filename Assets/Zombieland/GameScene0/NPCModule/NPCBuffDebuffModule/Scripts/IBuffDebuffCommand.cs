namespace Zombieland.GameScene0.NPCModule.NPCBuffDebuffModule
{
    public interface IBuffDebuffCommand : ICommand
    {
        BuffDebuffData BuffDebuffData { get; set; }

        void Destroy();

        DirectImpactData GetProcessedImpactValue(DirectImpactData buffDebuff);
    }
}
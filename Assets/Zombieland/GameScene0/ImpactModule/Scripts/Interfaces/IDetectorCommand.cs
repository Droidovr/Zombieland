namespace Zombieland.GameScene0.ImpactModule
{
    public interface IDetectorCommand : IImpactCommand
    {
        public bool ExecuteOnActivation { get; set; }
        public float DetectionRadius { get; set; }
    }
}

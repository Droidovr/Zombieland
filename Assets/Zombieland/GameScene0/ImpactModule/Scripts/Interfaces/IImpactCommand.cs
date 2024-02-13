namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpactCommand
    {
        public IImpactController ImpactController { get; set; }
        public void Init();
        public void Activate();
        public void Deactivate();
    }
}

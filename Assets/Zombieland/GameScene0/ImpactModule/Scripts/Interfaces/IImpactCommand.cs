namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpactCommand : ICommand
    {
        IImpactController ImpactController { get; set; }
        public void Init();
        public void Deactivate();
    }
}

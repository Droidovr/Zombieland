namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpactCommand : ICommand
    {
        public IImpactController ImpactController { get; set; }
        public void Init(); 
        public void Deactivate();
    }
}

namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpactController 
    {
        public float Speed { get; set; }
        public int Damage { get; set; }
        public float LifeTime { get; set; }
        public void ActivateObject();
    }
}
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.TakeDamageModule
{
    public interface ITakeDamageController
    {
        public void ProcessDamage(IImpactController impactController);
    }
}

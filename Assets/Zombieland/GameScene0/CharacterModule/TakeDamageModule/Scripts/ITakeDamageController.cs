using Zombieland.GameScene0.ProjectileModule;

namespace Zombieland.GameScene0.CharacterModule.TakeDamageModule
{
    public interface ITakeDamageController
    {
        public void ProcessDamage(IProjectileController projectileController);
    }
}

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IShotCommand : ICommand
    {
        IWeapon Weapon { get; }

        void Stop();
    }
}
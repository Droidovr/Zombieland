namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public interface IBuffDebuffCommand : ICommand
    {
        ICharacterController Controller { get; }

        void Execute();
        IImpact HandleImpact(Impact impact);
    }
}
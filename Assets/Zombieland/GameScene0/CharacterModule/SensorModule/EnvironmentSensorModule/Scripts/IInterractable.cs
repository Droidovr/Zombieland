namespace Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule
{
    public interface IInterractable
    {
        IController Controller { get; }

        void ToggleInterractable(bool isInRange);
        void Interract();
    }
}


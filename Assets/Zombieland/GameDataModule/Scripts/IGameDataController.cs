namespace Zombieland.GameDataModule
{
    public interface IGameDataController
    {
        void SaveDada<T>(string name,T data);
        T GetData<T>(string name);
    }
}

using UnityEngine;

namespace Zombieland.GameDataModule
{
    public class PlayerPrefsStorage : IStorage
    {
        public void SaveDada<T>(string name, T data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(name, json);
            PlayerPrefs.Save();
        }

        public T GetData<T>(string name)
        {
            T data = default;
            if (PlayerPrefs.HasKey(name))
            {
                var json = PlayerPrefs.GetString(name);
                data = JsonUtility.FromJson<T>(json);
            }
            else
            {
                Debug.LogError($"There is no save with the name {name}!");
            }
            
            return data;
        }
    }
}
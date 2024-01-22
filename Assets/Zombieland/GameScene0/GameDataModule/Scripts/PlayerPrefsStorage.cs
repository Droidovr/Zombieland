using UnityEngine;

namespace Zombieland.GameScene0.GameDataModule
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
            if (!PlayerPrefs.HasKey(name))
            {
                SaveDada(name, GetDataFromResources<T>(name));
            }
            var json = PlayerPrefs.GetString(name);
            data = JsonUtility.FromJson<T>(json);
            return data;
        }
        
        private T GetDataFromResources<T>(string name)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(name);
            if (textAsset == null)
            {
                Debug.LogError("Cannot find file at " + name);
                return default;
            }
            T data = JsonUtility.FromJson<T>(textAsset.text);
            return data;
        }
    }
}
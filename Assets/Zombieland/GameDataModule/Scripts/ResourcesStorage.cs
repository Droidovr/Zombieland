using System.IO;
using UnityEngine;

namespace Zombieland.GameDataModule
{
    public class ResourcesStorage : IStorage
    {
        public void SaveDada<T>(string name, T data)
        {
#if UNITY_EDITOR
            var fileName = name + ".txt";
            var filePath = Path.Combine(Application.dataPath, "Zombieland/GameDataModule/Resources", fileName);
            Debug.Log($"<color=blue>Save data to filepath: {filePath}</color>");
            var json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);
#endif
        }

        public T GetData<T>(string name)
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
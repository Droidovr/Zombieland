using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule.Test
{
    [CustomEditor(typeof(ImpactDataManager))]
    public class ImapactDataManagerEditor : Editor
    {
        private string _fileName = "ImpactDataTest";
    
        public override void OnInspectorGUI()
        {
            ImpactDataManager impactDataManager = (ImpactDataManager)target;
            
            if (GUILayout.Button("Save ImpactData"))
            {
                var fileName = _fileName + ".txt";
                var filePath = Path.Combine(Application.dataPath, "Zombieland/GameScene0/ImpactModule/Resources", fileName);
                Debug.Log($"Save data to filepath: {filePath}");
                var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented};
                var json = JsonConvert.SerializeObject(impactDataManager.ImpactData, settings);
                File.WriteAllText(filePath, json);
            }
        
            if (GUILayout.Button("Load ImpactData"))
            {
                var textAsset = Resources.Load<TextAsset>(_fileName);
                if (textAsset == null)
                    Debug.LogError("Cannot find file at " + _fileName);
                var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
                impactDataManager.ImpactData = JsonConvert.DeserializeObject<ImpactData>(textAsset.text, settings);
            }
        
            if (GUILayout.Button("Impact Execute"))
            {
                impactDataManager.ImpactData.DeliveryHandler.Activate();

            }
        }
    }
}

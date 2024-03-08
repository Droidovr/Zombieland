using System;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule.Test
{
    [CustomEditor(typeof(TestImpactCreator))]
    public class TestImapactCreatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            TestImpactCreator testImpactCreator = (TestImpactCreator)target;

            GUILayout.TextField(testImpactCreator.FileName);
            
            if (GUILayout.Button("Save ImpactData"))
            {
                var fileName = testImpactCreator.FileName + ".txt";
                var filePath = Path.Combine(Application.dataPath, "Zombieland/GameScene0/ImpactModule/Resources", fileName);
                Debug.Log($"Save data to filepath: {filePath}");
                var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented};
                var json = JsonConvert.SerializeObject(testImpactCreator.Impact, settings);
                File.WriteAllText(filePath, json);
            }
        }
    }
}

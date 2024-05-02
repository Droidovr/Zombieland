using System.IO;
using Newtonsoft.Json;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
namespace Zombieland.GameScene0.NPCManagerModule
{
    [CustomEditor(typeof(NpcSpawnJSONCreator))]
    public class NpcSpawnJSONCreatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            NpcSpawnJSONCreator npcSpawnJsonCreator = (NpcSpawnJSONCreator)target;

            if (GUILayout.Button("Save Npc Spawn Data"))
            {
                var fileName = "NpcSpawnData.txt";
                var filePath = Path.Combine(Application.dataPath, "Zombieland/GameScene0/NPCManagerModule/Resources", fileName);
                Debug.Log($"Save data to filepath: {filePath}");
                var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented};
                var json = JsonConvert.SerializeObject(npcSpawnJsonCreator.GetNpcSpawnDataList(), settings);
                File.WriteAllText(filePath, json);
            }
        }
    }
}
#endif
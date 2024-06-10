using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Numerics;



#if UNITY_EDITOR
namespace Zombieland.GameScene0.NPCManagerModule
{
    [CustomEditor(typeof(NPCSpawnCreator))]
    public class NPCSpawnCreatorEditor : Editor
    {
        private const string FILE_NAME = "NpcSpawnData.txt";
        private const string FILE_PATH = "Zombieland/GameScene0/NPCManagerModule/Resources";


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            NPCSpawnCreator myTarget = (NPCSpawnCreator)target;

            if (GUILayout.Button("Save"))
            {
                Debug.Log("Test");
            }

            if (GUILayout.Button("Load with JSON "))
            {
                string fullPath = Path.Combine(Application.dataPath, FILE_PATH, FILE_NAME);
                if (File.Exists(fullPath))
                {
                    string jsonContent = File.ReadAllText(fullPath);
                    myTarget.NPCSpawnDatas = JsonConvert.DeserializeObject<List<NPCSpawnData>>(jsonContent);

                    EditorUtility.SetDirty(myTarget);
                }
                else
                {
                    Debug.LogError("File does not exist: " + fullPath);
                }
            }
        }
    }
}
#endif
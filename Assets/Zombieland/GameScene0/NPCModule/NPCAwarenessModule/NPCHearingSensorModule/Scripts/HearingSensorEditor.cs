using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
namespace Zombieland.GameScene0.NPCModule.NPCHearingSensorModule
{
    [CustomEditor(typeof(HearingSensor))]
    public class HearingSensorEditor : Editor
    {
        public void OnSceneGUI()
        {
            var sensorVisualizer = target as HearingSensor;

            // draw the hearing range
            Handles.color = sensorVisualizer.HearingRangeColour;
            Handles.DrawSolidDisc(sensorVisualizer.transform.position, Vector3.up, sensorVisualizer.HearingRange);
        }
    }
}
#endif
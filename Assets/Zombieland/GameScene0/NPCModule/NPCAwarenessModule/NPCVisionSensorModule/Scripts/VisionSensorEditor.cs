using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
namespace Zombieland.GameScene0.NPCModule.NPCVisionSensorModule
{
    [CustomEditor(typeof(VisionSensor))]
    public class VisionSensorEditor : Editor
    {
        public void OnSceneGUI()
        {
            var sensorVisualizer = target as VisionSensor;

            // work out the start point of the vision cone
            Vector3 startPoint = Mathf.Cos(-sensorVisualizer.VisionConeAngle * 0.5f * Mathf.Deg2Rad) * sensorVisualizer.transform.forward + 
                                 Mathf.Sin(-sensorVisualizer.VisionConeAngle * 0.5f * Mathf.Deg2Rad) * sensorVisualizer.transform.right;

            // draw the vision cone
            Handles.color = sensorVisualizer.VisionConeColour;
            Handles.DrawSolidArc(sensorVisualizer.transform.position, Vector3.up, startPoint, sensorVisualizer.VisionConeAngle, 
                sensorVisualizer.VisionConeRange);
        }
    }
}
#endif
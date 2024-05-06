using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    public class NPCDataController : Controller, INPCDataController
    {
        public NPCData NPCData { get; private set; }
        public INPCController NPCController { get; private set; }


        public NPCDataController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }


        protected override void CreateHelpersScripts()
        {
            LoadDefaultValue();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void LoadDefaultValue()
        {
            NPCData = NPCController.NPCManagerController.RootController.GameDataController.GetData<NPCData>(NPCController.NPCSpawnData.NPCJsonFileName);
            NPCData.NPCSpawnData = NPCController.NPCSpawnData;

            Debug.Log($"Name: {NPCData.Name}, ID: {NPCData.ID}, PrefabName: {NPCData.PrefabName}, MaxHealth: {NPCData.MaxHealth}, CurrentHealth: {NPCData.CurrentHealth}, Speed: {NPCData.Speed}, StopDistance: {NPCData.StopDistance}, VisionAwarenessSpeed: {NPCData.VisionAwarenessSpeed}, HearingAwarenessSpeed: {NPCData.HearingAwarenessSpeed}, AwarenessDecaySpeed: {NPCData.AwarenessDecaySpeed}, MaxAwarenessLevel: {NPCData.MaxAwarenessLevel}");
        }
    }
}
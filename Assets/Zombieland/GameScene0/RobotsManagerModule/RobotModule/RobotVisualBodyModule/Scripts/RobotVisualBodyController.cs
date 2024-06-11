using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.NPCModule.NPCVisualBodyModule;



namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotVisualBodyModule
{
    public class RobotVisualBodyController : Controller, IRobotVisualBodyController
    {
        public event Action OnWeaponInSceneReady;

        public GameObject RobotInScene { get; private set; }
        public GameObject WeaponInScene { get; private set; }
        public List<GameObject> SensorTriggersGameobject { get; private set; }
        public IRobotController RobotController { get; private set; }


        private CreateRobotPrefab _createRobotPrefab;

        public RobotVisualBodyController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RobotController = parentController as IRobotController;
            _createRobotPrefab = new CreateRobotPrefab();
        }

        protected override void CreateHelpersScripts()
        {
            CreateRobotGameobject();

        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn�t have any subsystems at the moment.
        }


        private void CreateRobotGameobject()
        {
            RobotInScene = _createRobotPrefab.CreateRobot(this, Vector3.zero, Quaternion.identity);
            RobotInScene.SetActive(false);
        }
    }
}
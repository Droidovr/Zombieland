using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.CameraModule
{
    public class CameraController : Controller, ICameraController
    {
        private readonly IRootController _rootController;
        private InitializerCamera _initializerCamera;

        public Camera PlayerCamera { get; private set; }

        public CameraController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            _rootController = parentController as IRootController;
        }

        protected override void CreateHelpersScripts()
        {
            CreateCameraObject(_rootController.CharacterController.CharacterTransform);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void CreateCameraObject(Transform characterTransform)
        {
            _initializerCamera = new InitializerCamera();
            var cameraData = _rootController.GameDataController.GetData<CameraData>("CameraData");
            _initializerCamera.Init(cameraData, characterTransform);
            PlayerCamera = _initializerCamera.MainCamera;
        }

    }
}


using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.NPCModule.NPCHearingSensorModule.Scripts;
using Zombieland.GameScene0.NPCModule.NPCVisionSensorModule;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule
{
    public class NPCAwarenessController : Controller, INPCAwarenessController
    {
        public INPCController NPCController { get; set; }
        private INPCVisionSensorController _visionSensorController;
        private INPCHearingSensorController _hearingSensorController;

        private bool _isTargetInsideVisionZone;
        private bool _isTargetInsideHearingZone;
        private bool _isTargetFocused;

        private float _visionAwarenessSpeed;
        private float _hearingAwarenessSpeed;
        private float _awarenessDecaySpeed;
        private float _maxAwarenessLevel;
        private float _currentAwarenessLevel;

        private Updater _updater;

        public NPCAwarenessController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            NPCController = (INPCController) parentController;
            TestCreateSubsystems();
        }

        protected override void CreateHelpersScripts()
        {
            _visionAwarenessSpeed = NPCController.DataController.NPCData.visionAwarenessSpeed;
            _hearingAwarenessSpeed = NPCController.DataController.NPCData.hearingAwarenessSpeed;
            _awarenessDecaySpeed = NPCController.DataController.NPCData.awarenessDecaySpeed;
            _maxAwarenessLevel = NPCController.DataController.NPCData.maxAwarenessLevel;

            _updater = NPCController.VisualBodyController.ActiveNPC.GetComponent<Updater>();
            _updater.SubscribeToUpdate(UpdateVisibility);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            _visionSensorController = new NPCVisionSensorController(this, null);
            subsystemsControllers.Add((IController) _visionSensorController);
            _hearingSensorController = new NPCHearingSensorController(this, null);
            subsystemsControllers.Add((IController) _hearingSensorController);
        }

        private void TestCreateSubsystems()
        {
            _visionSensorController = new NPCVisionSensorController(this, null);
            _hearingSensorController = new NPCHearingSensorController(this, null);
        }

        public void CanSeeTarget(bool isTargetDetected)
        {
            _isTargetInsideVisionZone = isTargetDetected;
        }

        public void CanHearTarget(bool isTargetDetected)
        {
            _isTargetInsideHearingZone = isTargetDetected;
        }

        private void UpdateVisibility()
        {
            if (_isTargetInsideVisionZone)
            {
                _currentAwarenessLevel += _visionAwarenessSpeed * Time.deltaTime;
            }

            if (_isTargetInsideHearingZone)
            {
                _currentAwarenessLevel += _hearingAwarenessSpeed * Time.deltaTime;
            }

            if (!_isTargetInsideHearingZone && !_isTargetInsideVisionZone)
            {
                _currentAwarenessLevel -= _awarenessDecaySpeed * Time.deltaTime;
            }

            _currentAwarenessLevel = Mathf.Clamp(_currentAwarenessLevel, 0, _maxAwarenessLevel);

            if (_isTargetFocused && _currentAwarenessLevel <= 0f)
            {
                _isTargetFocused = false;
                // Call Lose Target
                return;
            }

            if (!_isTargetFocused && _currentAwarenessLevel >= _maxAwarenessLevel)
            {
                _isTargetFocused = true;
                // Call Follow Target
            }
        }
    }
}
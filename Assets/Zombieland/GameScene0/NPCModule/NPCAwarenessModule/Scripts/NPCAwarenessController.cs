using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.NPCModule.NPCHearingSensorModule.Scripts;
using Zombieland.GameScene0.NPCModule.NPCVisionSensorModule;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule
{
    public class NpcAwarenessController : Controller, INpcAwarenessController
    {
        public INpcController NpcController { get; }
        public bool IsTargetInFocus { get; private set; }
        public event Action<bool> OnTargetInFocus;
        private INpcVisionSensorController _visionSensorController;
        private INpcHearingSensorController _hearingSensorController;

        private bool _isTargetInsideVisionZone;
        private bool _isTargetInsideHearingZone;

        private float _visionAwarenessSpeed;
        private float _hearingAwarenessSpeed;
        private float _awarenessDecaySpeed;
        private float _maxAwarenessLevel;
        private float _currentAwarenessLevel;

        private Updater _updater;

        public NpcAwarenessController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            NpcController = (INpcController) parentController;
        }

        protected override void CreateHelpersScripts()
        {
            _visionAwarenessSpeed = NpcController.NpcDataController.NpcData.VisionAwarenessSpeed;
            _hearingAwarenessSpeed = NpcController.NpcDataController.NpcData.HearingAwarenessSpeed;
            _awarenessDecaySpeed = NpcController.NpcDataController.NpcData.AwarenessDecaySpeed;
            _maxAwarenessLevel = NpcController.NpcDataController.NpcData.MaxAwarenessLevel;

            _updater = NpcController.NpcVisualBodyController.NpcOnScene.GetComponent<Updater>();
            _updater.SubscribeToUpdate(UpdateVisibility);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            _visionSensorController = new NpcVisionSensorController(this, null);
            subsystemsControllers.Add((IController) _visionSensorController);
            _hearingSensorController = new NpcHearingSensorController(this, null);
            subsystemsControllers.Add((IController) _hearingSensorController);
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

            if (IsTargetInFocus && _currentAwarenessLevel <= 0f)
            {
                IsTargetInFocus = false;
                OnTargetInFocus?.Invoke(false);
                return;
            }

            if (!IsTargetInFocus && _currentAwarenessLevel >= _maxAwarenessLevel)
            {
                IsTargetInFocus = true;
                OnTargetInFocus?.Invoke(true);
            }
        }
    }
}
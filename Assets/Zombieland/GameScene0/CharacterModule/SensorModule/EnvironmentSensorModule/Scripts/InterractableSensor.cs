using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule
{
    public class InterractableSensor : MonoBehaviour
    {
        private List<IInterractable> _interractablesInRange;
        private IEnvironmentSensorController _environmentSensorController;

        public void Init(IController parentController)
        {
            _interractablesInRange = new List<IInterractable>();
            _environmentSensorController = parentController as IEnvironmentSensorController;
        }

        public void TryInterract()
        {
            if ( _interractablesInRange.Count == 0)
            {
                return;
            }
            if (_interractablesInRange[0].TryInterract(_environmentSensorController))
            {
                _interractablesInRange.RemoveAt(0);
            }
        }

        public void RemoveInterractable(IInterractable interractable)
        {
            _interractablesInRange.Remove(interractable);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IInterractable>(out var interractable))
            {
                interractable.ToggleInterractable(true);
                _interractablesInRange.Add(interractable);
                _environmentSensorController.InterractionTriggerEnter(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IInterractable>(out var interractable))
            {
                interractable.ToggleInterractable(false);
                _interractablesInRange.Remove(interractable);
                _environmentSensorController.InterractionTriggerEnter(false);
                // Add logic for proccessing being inside multiple triggers at once
            }
        }
    }
}


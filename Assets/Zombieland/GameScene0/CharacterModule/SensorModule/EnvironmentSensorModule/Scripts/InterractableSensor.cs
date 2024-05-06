using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule
{
    public class InterractableSensor : MonoBehaviour
    {
        private List<IInterractable> _interractablesInRange;

        public void Init()
        {
            _interractablesInRange = new List<IInterractable>();
            //add this to the character on scene
        }

        public void TryInterract()
        {
            // try to interract with the first Interractable through listening to the event in Controller
        }

        private void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IInterractable>(out var interractable))
            {
                interractable.ToggleInterractable(true);
                _interractablesInRange.Add(interractable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IInterractable>(out var interractable))
            {
                interractable.ToggleInterractable(false);
                _interractablesInRange.Remove(interractable);
            }
        }
    }
}


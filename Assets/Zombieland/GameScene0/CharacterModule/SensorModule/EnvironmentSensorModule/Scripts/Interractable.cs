using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule
{
    public class Interractable : MonoBehaviour, IInterractable
    {
        public IController Controller { get; private set; }

        private bool isInInterractionRange;

        public void Init(IController controller)
        {
            Controller = controller;
        }

        public void Interract()
        {

        }

        public void ToggleInterractable(bool isInRange)
        {
            isInInterractionRange = isInRange;
        }
    }
}



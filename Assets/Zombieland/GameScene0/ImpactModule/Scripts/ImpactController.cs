using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public class ImpactController : MonoBehaviour, IImpactController
    {
        public float Speed { get; set; }
        public int Damage { get; set; }
        public float LifeTime { get; set; }

        public void ActivateObject()
        {
            // Set active, start moving
        }

        private void OnTriggerEnter(Collider collider)
        {
            // raycast
            // get objects with IImpactable
            // call IImpactable.ApplyImpact(this)
        }
    }
}

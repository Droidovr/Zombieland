using System;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [RequireComponent(typeof(Rigidbody))]
    public class CollisionHandler : MonoBehaviour
    {
        private Collider _objectCollider;
        public event Action<Collider> OnObjectCollision;

        private bool _isInitialCollisionExcluded;

        private void Awake()
        {
            _objectCollider = GetComponent<Collider>();
        }

        public void Init(Action<Collider> onObjectCollision)
        {
            OnObjectCollision += onObjectCollision;
        }

        private void OnTriggerEnter(Collider targetObjectCollider)
        {
            if (!_isInitialCollisionExcluded)
            {
                Physics.IgnoreCollision(_objectCollider, targetObjectCollider, true);
                _isInitialCollisionExcluded = true;
                return;
            }
        
            OnObjectCollision?.Invoke(targetObjectCollider);
        }
    }
}

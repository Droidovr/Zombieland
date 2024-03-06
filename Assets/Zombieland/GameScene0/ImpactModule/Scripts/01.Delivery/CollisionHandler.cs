using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [RequireComponent(typeof(Rigidbody))]
    public class CollisionHandler : MonoBehaviour
    {
        private Action _onObjectCollision;
        public Collider TargetObjectCollider { get; private set; }

        public void Init(Action onObjectCollision, List<Collider> ignoringColliders)
        {
            _onObjectCollision = onObjectCollision;
            var objectCollider = GetComponent<Collider>();
            foreach (var ignoringCollider in ignoringColliders)
            {
                Physics.IgnoreCollision(objectCollider, ignoringCollider, true);
            }
        }

        private void OnTriggerEnter(Collider targetObjectCollider)
        {
            TargetObjectCollider = targetObjectCollider;
            _onObjectCollision?.Invoke();
        }
    }
}

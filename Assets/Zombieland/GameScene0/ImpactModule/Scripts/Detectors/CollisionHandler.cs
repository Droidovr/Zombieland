using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionHandler : MonoBehaviour
{
    private Action _objectCollided;
    private Action<Collider> _objectCollidedRef;

    public void Init(Action objectCollided)
    {
        _objectCollided = objectCollided;
    }
    
    public void Init(Action<Collider> objectCollidedRef)
    {
        _objectCollidedRef = objectCollidedRef;
    }

    private void OnTriggerEnter(Collider targetObjectCollider)
    {
        _objectCollided?.Invoke();
        _objectCollidedRef?.Invoke(targetObjectCollider);
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionHandler : MonoBehaviour
{
    private Action<Collider> _objectCollided;

    public void Init(Action<Collider> objectCollided)
    {
        _objectCollided = objectCollided;
    }

    private void OnTriggerEnter(Collider targetObjectCollider)
    {
        _objectCollided?.Invoke(targetObjectCollider);
    }
}

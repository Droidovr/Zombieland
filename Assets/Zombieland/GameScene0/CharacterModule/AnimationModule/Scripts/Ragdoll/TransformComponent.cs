using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class TransformComponent
    {
        public Transform Transform { get; private set; }

        public Quaternion PrivRotation;
        public Quaternion StoredRotation;

        public Vector3 PrivPosition;
        public Vector3 StoredPosition;

        public TransformComponent(Transform transform)
        {
            Transform = transform;
        }
    }
}
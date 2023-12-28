using UnityEngine;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public static class CharacterPhysicsInitializer
    {
        public static void AddRigidbodyComponent(GameObject gameObject)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        public static void AddColliderComponent(GameObject gameObject)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }
}
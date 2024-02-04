using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class RagdollComponent
    {
        public Rigidbody RigidBody { get; private set; }
        public CharacterJoint Joint { get; private set; }
        public Vector3 ConnectedAnchorDefault { get; private set; }

        public RagdollComponent(Rigidbody rigidbody)
        {
            RigidBody = rigidbody;

            Joint = rigidbody.GetComponent<CharacterJoint>();
            
            if (Joint != null)
            {
                ConnectedAnchorDefault = Joint.connectedAnchor;
            }
            else
            {
                ConnectedAnchorDefault = Vector3.zero;
            }
        }
    }
}
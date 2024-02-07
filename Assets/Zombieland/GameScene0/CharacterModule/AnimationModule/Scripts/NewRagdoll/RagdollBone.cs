using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class RagdollBone
    {
        public Transform BoneTransform;
        public Rigidbody BoneRigidbody;
        public Collider BoneCollider;
        public CharacterJoint BoneCharacterJoint;

        public RagdollBone(Transform child)
        {
            BoneTransform = child;
            BoneRigidbody = child.GetComponent<Rigidbody>();
            BoneCollider = child.GetComponent<Collider>();
            BoneCharacterJoint = child.GetComponent<CharacterJoint>();
        }
    }
}
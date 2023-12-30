namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class TestCharacterData
    {
        //Speed
        public float SpeedMoving = 5f;
        public float SpeedRotation = 5f;

        //Rigidbody
        public float Mass = 50;
        public float Drag = 1f;
        public float AngularDrag = 0.5f;
        public bool UseGravity = true;
        public bool IsKinematic = false;
        public bool ConstaintsFreezePositionX = false;
        public bool ConstaintsFreezePositionY = false;
        public bool ConstaintsFreezePositionZ = false;
        public bool ConstaintsFreezeRotationX = true;
        public bool ConstaintsFreezeRotationY = false;
        public bool ConstaintsFreezeRotationZ = true;
    }
}
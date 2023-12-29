public class DataCharacter
{
    //Speed
    public float SpeedMoving = 5f;
    public float SpeedRotation = 5f;

    //Rigidbody
    public float Mass = 1;
    public float Drag = 0;
    public float AngularDrag = 0.05f;
    public bool UseGravity = true;
    public bool IsKinematic = false;
    public bool ConstaintsFreezePositionX = false;
    public bool ConstaintsFreezePositionY = false;
    public bool ConstaintsFreezePositionZ = false;
    public bool ConstaintsFreezeRotationX = true;
    public bool ConstaintsFreezeRotationY = false;
    public bool ConstaintsFreezeRotationZ = true;
}

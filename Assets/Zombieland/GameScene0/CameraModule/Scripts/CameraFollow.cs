using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [field : SerializeField]
    public Transform CameraPivot0 { get; private set; }

    [field: SerializeField]
    public Transform CameraPivot1 { get; private set; }

    [field: SerializeField]
    public Transform MainCamera { get; private set; }

    private Transform _characterTransform;
    [SerializeField] private float _cameraSmoothSpeed = 0.125f;

    private void FixedUpdate()
    {
        //CameraPivot0.position = Vector3.Lerp(CameraPivot0.position, _characterTransform.position, _cameraSmoothSpeed);
    }


    public void SetCharacterTransform(Transform characterTransform)
    {
        _characterTransform = characterTransform;
    }

}

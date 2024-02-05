using UnityEngine;

namespace Zombieland.GameScene0.CameraModule
{ 
    public class InitializerCamera
    {
        private const string PREFAB_NAME = "CameraContainer";
        public void Init(CameraData cameraData, Transform characterTransform)
        {
            GameObject prefab = Resources.Load<GameObject>(PREFAB_NAME);
            GameObject cameraGO = GameObject.Instantiate(prefab);
            CameraFollow cameraFollow = cameraGO.GetComponent<CameraFollow>();
            cameraFollow.SetCharacterTransform(characterTransform);
            cameraFollow.CameraPivot0.position = characterTransform.position;
            Debug.Log($"Camera pivot0 rotation = {cameraData.CameraPivot0RotationY}");
            cameraFollow.CameraPivot0.rotation = Quaternion.Euler(0f, cameraData.CameraPivot0RotationY, 0f);
            cameraFollow.CameraPivot1.localPosition = new Vector3(0f,
                                                cameraData.CameraPivot1LocalPositionY, cameraData.CameraPivot1LocalPositionZ);
            cameraFollow.MainCamera.localRotation = Quaternion.Euler(cameraData.CameraLocalRotationX, 0f, 0f);
            cameraFollow.MainCamera.GetComponent<Camera>().orthographicSize = cameraData.CameraSize;
        }
    }
}


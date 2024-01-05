using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CreateCharacterGameobject : MonoBehaviour
    {
        public GameObject CharacterGameobject => _characterGameObject;

        private GameObject _prefab;
        //private string _prefabName = "Policewoman";
        //private Vector3 _positionInstantiatePrefab = new Vector3(0, 0, 0);
        private string _prefabName = "Character";
        private Vector3 _positionInstantiatePrefab = new Vector3(0, 1f, 0);
        private GameObject _characterGameObject;

        public void Init()
        {
            _prefab = Resources.Load<GameObject>(_prefabName);
            _characterGameObject = Instantiate(_prefab, _positionInstantiatePrefab, Quaternion.identity);

            // While there is no camera controller, I did this.
            Camera.main.GetComponent<MovingCamera>().Character = _characterGameObject;
        }
    }
}

using System.Collections;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class TestStartSystem : MonoBehaviour
    {
        private UIController _uIController;

        // Start is called before the first frame update
        void Start()
        {
            _uIController = new UIController(null, null);
            _uIController.Enable();

            StartCoroutine(Debuger());
        }

        private void OnDestroy()
        {
            _uIController.Disable();
        }

        private IEnumerator Debuger()
        {
            yield return new WaitForSeconds(2);

            Debug.Log(_uIController.IsActive);
        }
    }
}

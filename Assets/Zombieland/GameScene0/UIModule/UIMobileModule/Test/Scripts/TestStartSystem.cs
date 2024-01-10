using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class TestStartSystem : MonoBehaviour
    {
        private UIController _uIController;

        private void Start()
        {
            //_uIController = new UIController(null, null);
            //_uIController.Enable();

            var creator = new Creator();
            creator.Create();
        }
    }
}

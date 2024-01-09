using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class TestStartSystem : MonoBehaviour
    {
        private IUIController _uIController;

        private void Start()
        {
            _uIController = new UIController(null, null);
        }
    }
}

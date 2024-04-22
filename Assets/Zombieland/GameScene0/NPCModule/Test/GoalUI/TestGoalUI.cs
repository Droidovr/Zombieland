using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zombieland.GameScene0.NPCModule
{
    public class TestGoalUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI Name;
        [SerializeField] Slider Priority;
        [SerializeField] TextMeshProUGUI Status;

        public void UpdateGoalInfo(string _name, string _status, float _priority)
        {
            Name.text = _name;
            Status.text = _status;
            Priority.value = _priority;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule
{
    public class TestGoalsUIManager : MonoBehaviour
    {
        [SerializeField] GameObject GoalPrefab;
        [SerializeField] RectTransform GoalRoot;

        Dictionary<MonoBehaviour, TestGoalUI> DisplayedGoals = new Dictionary<MonoBehaviour, TestGoalUI>();

        public void UpdateGoal(MonoBehaviour goal, string _name, string _status, float _priority)
        {
            // add if not present
            if (!DisplayedGoals.ContainsKey(goal))
                DisplayedGoals[goal] = Instantiate(GoalPrefab, Vector3.zero, Quaternion.identity, GoalRoot).GetComponent<TestGoalUI>();

            DisplayedGoals[goal].UpdateGoalInfo(_name, _status, _priority);
        }
    }
}

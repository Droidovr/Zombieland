namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public interface IGoal
    {
        public bool CanRun();
        public void OnGoalActivated();
        public void GoalTick();
        public int CalculatePriority();
        public void OnGoalDeactivated();
    }
}

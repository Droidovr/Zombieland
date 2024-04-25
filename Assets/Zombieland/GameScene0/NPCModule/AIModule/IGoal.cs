namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public interface IGoal
    {
        public void Init(INpcController NpcController);
        public bool CanRun();
        public void OnGoalActivated();
        public void OnTickGoal();
        public int CalculatePriority();
        public void OnGoalDeactivated();
    }
}

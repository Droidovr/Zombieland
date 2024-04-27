using System;

namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public class ActionIdle : ActionBase
    {
        private System.Type[] supportedGoals = new[] {typeof(GoalIdle)};

        public override System.Type[] GetSupportedGoals()
        {
            return supportedGoals;
        }

        public override float Cost()
        {
            return 0f;
        }
    }
}

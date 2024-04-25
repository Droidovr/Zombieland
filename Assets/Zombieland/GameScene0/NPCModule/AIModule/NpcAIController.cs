using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public class NpcAIController : Controller, INpcAIController
    {
        private readonly INpcController _npcController;
        private List<IGoal> _goalsList;

        public NpcAIController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _npcController = (INpcController) parentController;
            TestCreateHelpersScripts();
        }

        protected override void CreateHelpersScripts()
        {
            _goalsList = new List<IGoal>(_npcController.VisualBodyController.ActiveNPC.GetComponentsInChildren<IGoal>());
            foreach (var goal in _goalsList)
            {
                goal.Init(_npcController);
            }
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
        
        private void TestCreateHelpersScripts()
        {
            _goalsList = new List<IGoal>(_npcController.VisualBodyController.ActiveNPC.GetComponentsInChildren<IGoal>());
            foreach (var goal in _goalsList)
            {
                goal.Init(_npcController);
            }
        }

    }
}

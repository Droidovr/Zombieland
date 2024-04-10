using System.Collections.Generic;
using Zombieland;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.NPCModule.NPCTakeImpactModule;

public class NPCTakeImpactController : Controller, INPCTakeImpactController
{
    public NPCTakeImpactController(IController parentController, List<IController> requiredControllers) 
        : base(parentController, requiredControllers)
    {
    }

    protected override void CreateHelpersScripts()
    {
        throw new System.NotImplementedException();
    }

    protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
    {
        throw new System.NotImplementedException();
    }

    public void ApplyImpact(List<DirectImpactData> impact)
    {
        throw new System.NotImplementedException();
    }
}

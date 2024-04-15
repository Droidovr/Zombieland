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
        //This method has no implementation
    }

    protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
    {
        //This method has no implementation
    }

    public void ApplyImpact(List<DirectImpactData> impact)
    {
        //This method has no implementation
    }
}

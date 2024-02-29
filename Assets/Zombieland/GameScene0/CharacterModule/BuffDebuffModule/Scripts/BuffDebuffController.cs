using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public class BuffDebuffController : Controller, IBuffDebuffController
    {
        public Dictionary<string, IBuffDebuffCommand> Buffs;
        public Dictionary<string, IBuffDebuffCommand> Debuffs;

        public BuffDebuffController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }

        public void InjectBuffs(List<IBuffDebuffCommand> buffs)
        {
            foreach (var buff in buffs)
            {
                if (!Buffs.ContainsKey(buff.Name))
                {
                    Buffs.Add(buff.Name, buff);
                    buff.Execute();
                }
            }
        }

        public void InjectDebuffs(List<IBuffDebuffCommand> debuffs)
        {
            foreach (var debuff in debuffs)
            {
                if (!Debuffs.ContainsKey(debuff.Name))
                {
                    Debuffs.Add(debuff.Name, debuff);
                    debuff.Execute();
                }
            }
        }

        public float GetProcessedImpactValue(BuffDebuff buffDebuff)
        {

            return 5;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
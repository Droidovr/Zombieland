using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public class BuffDebuffController : Controller, IBuffDebuffController
    {
        public Dictionary<string, ICommand> Buffs;
        public Dictionary<string, ICommand> Debuffs;

        public BuffDebuffController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }

        public void InjectBuffs(List<ICommand> buffs)
        {
            foreach (var buff in buffs)
            {
                if (!Buffs.ContainsKey(buff.Name))
                {
                    Buffs.Add(buff.Name, buff);
                }
            }
        }

        public void InjectDebuffs(List<ICommand> debuffs)
        {
            foreach (var debuff in debuffs)
            {
                if (!Debuffs.ContainsKey(debuff.Name))
                {
                    Debuffs.Add(debuff.Name, debuff);
                }
            }
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
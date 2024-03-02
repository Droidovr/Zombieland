using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public class BuffDebuffController : Controller, IBuffDebuffController
    {
        public Dictionary<string, IBuffDebuffCommand> Buffs { get; set; }
        public Dictionary<string, IBuffDebuffCommand> Debuffs { get; set; }

        public ICharacterController CharacterController { get; private set; }

        public BuffDebuffController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public void InjectBuffs(List<IBuffDebuffCommand> buffs)
        {
            foreach (var buff in buffs)
            {
                if (!Buffs.ContainsKey(buff.Name))
                {
                    Buffs.Add(buff.Name, buff);
                    buff.buffDebuffController = this;
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
                    debuff.buffDebuffController = this;
                    debuff.Execute();
                }
            }
        }

        public SingleImpact GetProcessedImpactValue(SingleImpact buffDebuff)
        {
            SingleImpact localBuffDebuff = buffDebuff;

            foreach (var buff in Buffs.Values)
            {
                localBuffDebuff = buff.GetProcessedImpactValue(localBuffDebuff);
            }

            foreach (var debuff in Debuffs.Values)
            {
                localBuffDebuff = debuff.GetProcessedImpactValue(localBuffDebuff);
            }

            return localBuffDebuff;
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
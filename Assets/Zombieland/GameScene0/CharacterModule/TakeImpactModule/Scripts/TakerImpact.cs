using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.CharacterModule.TakeImpactModule
{
    public class TakerImpact
    {
        private readonly ICharacterController _characterController;

        public TakerImpact(ICharacterController characterController)
        { 
            _characterController = characterController;
        }

        public void ApplyImpact(List<DirectImpactData> damageTakens)
        {
            if (!_characterController.CharacterDataController.CharacterData.IsDead)
            {
                for (int i = 0; i < damageTakens.Count; i++)
                {
                    DirectImpactData damageTakenBuffDebuff = _characterController.BuffDebuffController.GetProcessedImpactValue(damageTakens[i]);

                    if (damageTakenBuffDebuff.AbsoluteValue > 0)
                    {
                        _characterController.CharacterDataController.CharacterData.HP -= damageTakenBuffDebuff.AbsoluteValue;
                    }

                    if (damageTakenBuffDebuff.PercentageValue > 0)
                    {
                        _characterController.CharacterDataController.CharacterData.HP -= _characterController.CharacterDataController.CharacterData.HP / 100 * damageTakenBuffDebuff.PercentageValue;
                    }
                }
            }
        }
    }
}
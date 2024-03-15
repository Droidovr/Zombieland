using Assets.Zombieland.GameScene0.CharacterModule.TakeImpactModule.Scripts.ImpactProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;

namespace Assets.Zombieland.GameScene0.CharacterModule.TakeImpactModule.Scripts
{
    internal class TakeImpactHandler
    {

        private Boolean _init;
        private IBuffDebuffController _buffDebuffController;
        private CharacterData _characterData;
        private Dictionary<DirectImpactType, IImpactProvider> _impactProviders;
        public TakeImpactHandler() {
          
        }

        public void Init(ICharacterController _characterController)
        {
            _buffDebuffController = _characterController.BuffDebuffController;
          
            var _defaultProvider = new FixedDamageProvider(_characterController.CharacterDataController.CharacterData);
           
            _impactProviders = new Dictionary<DirectImpactType, IImpactProvider>
            {
                {DirectImpactType.None, _defaultProvider },
                {DirectImpactType.Poison, _defaultProvider },
                {DirectImpactType.ParameterDecreation, _defaultProvider }
            };


        }

        public void handleImpact(DirectImpactData directImpactData)
        {
            _buffDebuffController.GetProcessedImpactValue(directImpactData);
            _impactProviders[directImpactData.Type].provideImpact(directImpactData);

        }

    }
}

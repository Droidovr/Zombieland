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
        private Dictionary<DirectImpactType, IImpactProvider> _impactProviders;
        private IImpactProvider _defaultProvider;

        public void Init(ICharacterController _characterController)
        {
            _buffDebuffController = _characterController.BuffDebuffController;
          
            _defaultProvider = new FixedDamageProvider(_characterController.CharacterDataController.CharacterData);
            _impactProviders = new Dictionary<DirectImpactType, IImpactProvider>();
            initProviders();

        }

        private void initProviders()
        {
            // Add here new Providers to _impactProviders depend on DirectImpactType
        }

        public void handleImpact(DirectImpactData directImpactData)
        {
           var updatedImpact = _buffDebuffController.GetProcessedImpactValue(directImpactData);
           var impactProvider = _impactProviders.TryGetValue(directImpactData.Type, out var value) ? value : _defaultProvider;

           impactProvider.provideImpact(updatedImpact);
        }

    }
}

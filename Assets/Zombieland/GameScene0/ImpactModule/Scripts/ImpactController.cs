using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.ImpactModule
{
    
    public class ImpactController : IImpactController
    {
        public Vector3 SpawnPosition { get; set; }
        public Quaternion InitialRotation { get; set; }
        public Transform TargetTransform { get; set; }
        public List<IImpactable> TargetImpactableList { get; set; }
        public ImpactData ImpactData { get; set; }

        private readonly IRootController _rootController;

        public ImpactController(string impactName, IRootController rootController)
        {
            _rootController = rootController;
            TargetImpactableList = new List<IImpactable>();
            
            //ImpactData = _rootController.GameDataController.GetData<ImpactData>(impactName);
            //Test Implementation
            var textAsset = Resources.Load<TextAsset>(impactName);
            if (textAsset == null)
                Debug.LogError("Cannot find file at " + impactName);
            var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
            ImpactData = JsonConvert.DeserializeObject<ImpactData>(textAsset.text, settings);

            ImpactData.DeliveryHandler.ImpactController = this;
            ImpactData.DeliveryHandler.Init();
        }

        public void Activate()
        {
            ImpactData.DeliveryHandler.Execute();
        }

        public void Deactivate()
        {
            ImpactData.DeliveryHandler.Deactivate();
            // Return to pool Logic
        }
    }
}

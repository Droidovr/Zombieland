using Newtonsoft.Json;

namespace Zombieland.GameScene0.ImpactModule
{
    
    public class Impact : IImpact
    {
        [JsonIgnore]
        public IController ImpactOwner { get; set; }
        public IImpactCommand Assembler { get; set; }
        public IImpactCommand Delivery { get; set; }
        public IImpactCommand DirectImpact { get; set; }
        public IImpactCommand BuffDebuffInjection { get; set; }

        public void Activate()
        {
            Assembler.Impact = this;
            Delivery.Impact = this;
            DirectImpact.Impact = this;
            BuffDebuffInjection.Impact = this;
            Assembler.Execute();
        }

        public void Deactivate()
        {
            Assembler.Deactivate();
            Delivery.Deactivate();
            DirectImpact.Deactivate();
            BuffDebuffInjection.Deactivate();
        }
        
        public Impact()
        {
            Assembler = new ObjectAssembler{PrefabName = "PrefabName"};
            Delivery = new MovingForwardHandler{MovingSpeed = 0f, Range = 0f, Lifetime = 0f};
            DirectImpact = new DefaultImpact{Detector = new UpfrontRayDetector{DetectionRadius = 0f}, Points = 0f, 
                OnTargetEffectPrefabName = "OnTargetEffectPrefabName", NoTargetEffectPrefabName = "NoTargetEffectPrefabName"};
            BuffDebuffInjection = new BuffDebuffInjection();
        }
    }

}


// public Impact(string impactName, IRootController rootController)
// {
//     _rootController = rootController;
//     TargetImpactableList = new List<IImpactable>();
//     
//     //ImpactData = _rootController.GameDataController.GetData<ImpactData>(impactName);
//     //Test Implementation
//     var textAsset = Resources.Load<TextAsset>(impactName);
//     if (textAsset == null)
//         Debug.LogError("Cannot find file at " + impactName);
//     var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
//     ImpactData = JsonConvert.DeserializeObject<ImpactData>(textAsset.text, settings);
//
//     ImpactData.DeliveryHandler.Impact = this;
//     ImpactData.DeliveryHandler.Init();
// }
using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class TestImpactData
    {
        public string ID; // Serializable
        public string Name; // Serializable
        public string IconID; // Serializable
        public List<TestConsumableResource> ConsumableResources; // Serializable
        public ICharacterController Owner;
        public List<IImpactable> Targets;
    }
}
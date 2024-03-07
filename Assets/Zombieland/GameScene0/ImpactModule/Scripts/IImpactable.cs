using UnityEngine;
using Zombieland.GameScene0.CharacterModule;

namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpactable
    {
        public ICharacterController Owner { get; set; }
        public Transform Transform  { get; }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterMovingController : Controller, ICharacterMovingController
    {
        public event Action<Vector2> OnMoved;

        private readonly IRootController _characterController;

        public CharacterMovingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            _characterController = parentController as IRootController;
        }

        protected override void CreateHelpersScripts()
        {
            
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            
        }
    }
}

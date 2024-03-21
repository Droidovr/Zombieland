using System;
using System.Collections.Generic;
using System.Numerics;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    public class SpawnDeathRespawnController : Controller, ISpawnDeathRespawnController
    {
        public event Action<Vector3> OnSpawn;

        private SpawnHelper spawnHelper;

        public ICharacterController CharacterController { get; }

        public SpawnDeathRespawnController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
            spawnHelper = new SpawnHelper(this);
        }

        protected override void CreateHelpersScripts()
        {
            spawnHelper.Start();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
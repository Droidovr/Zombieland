using System;
using System.Collections.Generic;
using System.Numerics;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    public class SpawnDeathRespawnController : Controller, ISpawnDeathRespawnController
    {
        public event Action<Vector3> OnSpawn;
        public ICharacterController CharacterController { get; }

        public SpawnDeathRespawnController(IController parentController, List<IController> requiredControllers) : base(
            parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        protected override void CreateHelpersScripts()
        {
            SpawnHelper spawnHelper = new SpawnHelper(this);
            spawnHelper.Start();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
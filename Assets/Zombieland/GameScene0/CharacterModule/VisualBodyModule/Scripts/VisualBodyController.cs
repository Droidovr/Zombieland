using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class VisualBodyController : Controller, IVisualBodyController
    {
        public GameObject CharacterGameobject { get; private set; }

        public VisualBodyController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class�s constructor doesn�t have any content yet.
        }

        protected override void CreateHelpersScripts()
        {
            CreateCharacterGameobject();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn�t have any subsystems at the moment.
        }

        private void CreateCharacterGameobject()
        {
            // Get SpawnPosition from GameManager or Gamedata && Add a dependency System in Character for VisualBodyController
            Vector3 spawnPositionCharacter = Vector3.zero;

            CreateCharacterGameobject createCharacterGameobject = new CreateCharacterGameobject();
            CharacterGameobject = createCharacterGameobject.CreateCharacter(spawnPositionCharacter);
        }
    }
}

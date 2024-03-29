using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class VisualBodyController : Controller, IVisualBodyController
    {
        public GameObject CharacterInScene { get; private set; }
        public List<GameObject> SensorTriggersGameobject { get; private set; }

        public VisualBodyController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class's constructor doesn't have any content yet.
        }

        protected override void CreateHelpersScripts()
        {
            CreateCharacterGameobject();

            GertterTriggers gertterTriggers = new GertterTriggers(this);
            SensorTriggersGameobject = gertterTriggers.GetSensorTriggers();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }

        private void CreateCharacterGameobject()
        {
            // Get SpawnPosition & SpanwRotation from GameManager or Gamedata && Add a dependency System in Character for VisualBodyController - !!! position + rotation
            Vector3 spawnPositionCharacter = new Vector3(-10f, 6.1f, -4f); // Test
            Quaternion spawnRotationCharacter = Quaternion.identity; // Test

            CreateCharacterPrefab createCharacterGameobject = new CreateCharacterPrefab();
            CharacterInScene = createCharacterGameobject.CreateCharacter(spawnPositionCharacter, spawnRotationCharacter);
        }
    }
}

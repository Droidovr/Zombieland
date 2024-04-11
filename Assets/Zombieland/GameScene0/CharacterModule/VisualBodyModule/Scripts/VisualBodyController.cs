using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class VisualBodyController : Controller, IVisualBodyController
    {
        public GameObject CharacterInScene { get; private set; }
        public GameObject WeaponInScene { get; private set; }
        public List<GameObject> SensorTriggersGameobject { get; private set; }
        public ICharacterController CharacterController {  get; private set; }

        public VisualBodyController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public override void Disable()
        {
            CharacterController.EquipmentController.OnWeaponChanged -= WeaponChangedHandler;

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            CreateCharacterGameobject();

            GertterTriggers gertterTriggers = new GertterTriggers(this);
            SensorTriggersGameobject = gertterTriggers.GetSensorTriggers();

            CharacterController.EquipmentController.OnWeaponChanged += WeaponChangedHandler;
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

        private void WeaponChangedHandler(Weapon weapon)
        {
            //WeaponInScene = new CreateWeaponPrefab().CtreateWeapon(weapon, this);
        }
    }
}

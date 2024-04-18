using System.Collections.Generic;
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

        private Weapon _currentWeapon;
        private CreateWeaponPrefab _createWeaponPrefab;

        public VisualBodyController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public override void Disable()
        {
            CharacterController.EquipmentController.OnWeaponChanged -= WeaponChangedHandler;
            CharacterController.AnimationController.OnCreateWeaponPrefab -= CreateWeaponPrefabHandler;
            CharacterController.AnimationController.OnDestroyWeaponPrefab -= DestroyWeaponPrefabHandler;

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            CreateCharacterGameobject();

            GertterTriggers gertterTriggers = new GertterTriggers(this);
            SensorTriggersGameobject = gertterTriggers.GetSensorTriggers();

            CharacterController.EquipmentController.OnWeaponChanged += WeaponChangedHandler;
            CharacterController.AnimationController.OnCreateWeaponPrefab += CreateWeaponPrefabHandler;
            CharacterController.AnimationController.OnDestroyWeaponPrefab += DestroyWeaponPrefabHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }

        private void CreateCharacterGameobject()
        {
            // Get SpawnPosition & SpanwRotation from GameManager or Gamedata && Add a dependency System in Character for VisualBodyController - !!! position + rotation
            Vector3 spawnPositionCharacter = new Vector3(3f, 0f, -4f); // Test
            Quaternion spawnRotationCharacter = Quaternion.identity; // Test

            CreateCharacterPrefab createCharacterGameobject = new CreateCharacterPrefab();
            CharacterInScene = createCharacterGameobject.CreateCharacter(spawnPositionCharacter, spawnRotationCharacter);
        }

        private void WeaponChangedHandler(Weapon weapon)
        {
            _currentWeapon = weapon;
        }

        private void CreateWeaponPrefabHandler()
        {
            _createWeaponPrefab = new CreateWeaponPrefab();

            WeaponInScene = _createWeaponPrefab.CtreateWeapon(_currentWeapon, CharacterInScene.GetComponent<Transform>());
            CharacterController.WeaponController.WeaponPointFire = WeaponInScene.GetComponent<Transform>().Find("PointFire");
        }

        private void DestroyWeaponPrefabHandler()
        {
            _createWeaponPrefab.DestroyWeapon(WeaponInScene);
        }
    }
}

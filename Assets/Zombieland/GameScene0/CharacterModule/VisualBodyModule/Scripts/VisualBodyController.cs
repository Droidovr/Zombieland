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
        public Transform WeaponPointFire { get; private set; }
        public AudioSource WeaponSoundFire { get; private set; }
        public ParticleSystem WeaponVFX { get; private set; }
        public List<GameObject> SensorTriggersGameobject { get; private set; }
        public ICharacterController CharacterController {  get; private set; }

        private Weapon _currentWeapon;
        private Weapon _cashWeaponDestroy;

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
            _cashWeaponDestroy = _currentWeapon;
            _currentWeapon = weapon;
        }

        private void CreateWeaponPrefabHandler()
        {
            WeaponInScene = new CreateWeaponPrefab().CtreateWeapon(_currentWeapon, CharacterInScene.GetComponent<Transform>());
            WeaponPointFire = WeaponInScene.GetComponent<Transform>().Find("PointFire");
            WeaponSoundFire = WeaponInScene.GetComponent<AudioSource>();
            Transform vfxTransform = WeaponInScene.GetComponent<Transform>().Find("VFX");
            if (vfxTransform != null)
            {
                WeaponVFX = vfxTransform.GetComponent<ParticleSystem>();
            }
        }

        private void DestroyWeaponPrefabHandler()
        {
            WeaponPointFire = null;
            WeaponSoundFire = null;
            WeaponVFX = null;

            if (WeaponInScene != null)
            {
                Debug.Log("DestroyWeaponPrefabHandler: " + _cashWeaponDestroy.WeaponData.Name);
                GameObject.Destroy(WeaponInScene);
            }
        }
    }
}

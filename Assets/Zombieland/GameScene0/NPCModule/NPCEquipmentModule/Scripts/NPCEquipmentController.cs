using System;
using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.EquipmentModule;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.NPCModule.NPCEquipmentModule
{
    public class NPCEquipmentController : Controller, INPCEquipmentController
    {
        public event Action<Weapon> OnWeaponChanged;
        public event Action<string> OnEquipmentChanged;
        public event Action OnImpactDepleted;

        public INPCController NPCController { get; private set; }
        //public Dictionary<int, WeaponSlot> WeaponSlots { get; private set; }
        public List<WeaponSlot> WeaponSlots { get; private set; }
        public string CurrentImpactID { get; private set; }
        public int CurrentImpactCount
        {
            get { return _currentImpactCount; }
            set { if (value <= 0) return; else _currentImpactCount = value; }
        }
        public string CurrentOutfitEquipped { get; private set; }


        private Weapon _currentWeaponEquipped;
        private int _currentActiveSlotIndex;
        private int _currentImpactCount;


        public NPCEquipmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
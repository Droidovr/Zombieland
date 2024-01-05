using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class TestVisualBodyController : Controller, ITestVisualBodyController
    {
        public ICharacterController CharacterController { get; }

        private CreateCharacterGameobject _characterGameobject;

        public TestVisualBodyController(IController parentController) 
        {
            Debug.Log("<color=blue>" + parentController.GetType() + "</color>");
            CharacterController = (ICharacterController) parentController;
        }

        public GameObject GetCharacterGameobject()
        {
            return _characterGameobject.CharacterGameobject;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            Debug.Log("TestVisualBodyController CreateSubsystems");
            
            _characterGameobject = new CreateCharacterGameobject();
            _characterGameobject.Init();
        }
    }
}
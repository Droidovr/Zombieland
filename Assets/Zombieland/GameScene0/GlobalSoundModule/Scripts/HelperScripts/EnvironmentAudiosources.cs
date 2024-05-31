using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.GlobalSoundModule
{
    public class EnvironmentAudiosources
    {
        private IGlobalSoundController _globalSoundController;


        public EnvironmentAudiosources(IGlobalSoundController globalSoundController)
        {
            _globalSoundController = globalSoundController;
            _globalSoundController.RootController.EnvironmentController.OnSceneLoaded += SceneLoadedHandler;
        }

        private void SceneLoadedHandler()
        {
            //_globalSoundController.RootController.EnvironmentController.GameobjectInScene.Phone.AddComponent<AudioDistanceController>();


            //public GameObject Phone;
            //public GameObject MineWithFire;
            //public GameObject Creamatorium;
            //public GameObject Printer;
            //public GameObject Server1;
            //public GameObject Server2;
            //public GameObject TualetMan;
            //public GameObject TualetWoman;
            //public GameObject ShowerRoomMan;
            //public GameObject ShowerRoomWoman;
        }
    }
}
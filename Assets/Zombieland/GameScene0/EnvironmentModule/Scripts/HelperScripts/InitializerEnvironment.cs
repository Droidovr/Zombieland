using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zombieland.GameScene0.EnvironmentModule
{
    public class InitializerEnvironment
    {
        public event Action OnSceneLoaded;

        public void Init(EnvironmentData environmentData)
        {
            SceneManager.LoadScene(environmentData.CurrentLevelName, LoadSceneMode.Additive);
            Application.targetFrameRate = 60;
        }
    }
}


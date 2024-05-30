using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zombieland.GameScene0.EnvironmentModule
{
    public class InitializerEnvironment
    {
        public event Action OnSceneLoaded;

        public void Init(EnvironmentData environmentData)
        {
            //SceneManager.LoadScene(environmentData.CurrentLevelName, LoadSceneMode.Additive);

            LoadSceneAsync(environmentData.CurrentLevelName).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Debug.LogError("<color=red>Error load scene: " + t.Exception + "</color>");
                }
            });

            Application.targetFrameRate = 60;
        }

        private async Task LoadSceneAsync(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                await Task.Yield();
            }

            OnSceneLoaded?.Invoke();
        }
    }
}


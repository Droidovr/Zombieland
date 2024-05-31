using UnityEngine;


namespace Zombieland.GameScene0.GlobalSoundModule
{
    public class AudioDistanceController : MonoBehaviour
    {
        private const float INVOKE_REPEATING_TIME = 0.1f;

        private IGlobalSoundController _globalSoundController;

        public void Init(IGlobalSoundController globalSoundController)
        {
            _globalSoundController = globalSoundController;
            InvokeRepeating(nameof(OnMouseOver), 0f, INVOKE_REPEATING_TIME);
        }


        private void OnMouseOver()
        {
            
        }
    }
}
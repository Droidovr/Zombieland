using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts
{
    [CreateAssetMenu(fileName = "AudioID", menuName = "Audio/Sound Data")]
    public class AudioIDAsset : ScriptableObject
    {
        [SerializeField] private string _walkSoundName;
        [SerializeField] private string _impactSoundName;

        public string walkSoundName => _walkSoundName;
        public string impactSoundName => _impactSoundName;
    }
}
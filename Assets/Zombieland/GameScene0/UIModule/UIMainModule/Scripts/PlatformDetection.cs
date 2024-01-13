using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public static class PlatformDetection
    {
        public static PlatformType GetPlatformType()
        {
            RuntimePlatform platform = Application.platform;

            switch (platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.LinuxPlayer:
                    return PlatformType.PC;

                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.MetroPlayerX86:
                case RuntimePlatform.MetroPlayerX64:
                case RuntimePlatform.MetroPlayerARM:
                    return PlatformType.Mobile;

                case RuntimePlatform.PS4:
                case RuntimePlatform.XboxOne:
                    return PlatformType.Console;

                default:
                    return PlatformType.Unknown;
            }
        }
    }
}
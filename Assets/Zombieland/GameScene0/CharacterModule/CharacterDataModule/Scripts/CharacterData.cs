using System;
using System.Numerics;
using Zombieland.GameScene0.CharacterModule.SpawnDeathRespawmModule;

namespace Zombieland.GameScene0.CharacterModule.CharacterDataModule
{
    public class CharacterData
    {
        public float MaxMovingSpeed;
        public float DesignMovingSpeed;
        public float MaxRotationSpeed;
        public float DesignRotationSpeed;


        public ICommandSpawner SpawnMethod;
    }
}
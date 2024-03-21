using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
  [Serializable]
  public class SpawnData
  {
    public Vector3 DefaultPosition { get; set; }
    public float SpawnRadius { get; set; }
    public SpawnType SpawnType { get; set; }
    
  }
}
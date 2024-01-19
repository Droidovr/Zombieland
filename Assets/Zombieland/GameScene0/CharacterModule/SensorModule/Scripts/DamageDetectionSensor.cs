using System;
using UnityEngine;
using Zombieland.GameScene0.ProjectileModule;

namespace Zombieland.GameScene0.CharacterModule.SensorModule
{
    public class DamageDetectionSensor : MonoBehaviour
    {
       private Action<IProjectileController> _onTakingDamage;

       public void Init(Action<IProjectileController> onTakingDamage)
       {
           _onTakingDamage = onTakingDamage;
       }

       public void TakeDamage(IProjectileController projectileController)
       {
           _onTakingDamage?.Invoke(projectileController);
       }
    }
}

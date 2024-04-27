using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public class ActionBase : MonoBehaviour
    {
        protected INpcController npcController;
        protected GoalBase linkedGoal;

        public virtual System.Type[] GetSupportedGoals()
        {
            return null;
        }
        
        public void Init(INpcController NpcController)
        {
            this.npcController = NpcController;
        }
        
        private void Update()
        {
            if(npcController == null) return;
            OnTick();
        }
    
        public virtual float Cost()
        {
            return 0f;
        }

        public virtual void OnActivated()
        {
        
        }

        public virtual void OnDeactivated()
        {
        
        }

        public virtual void OnTick()
        {
        
        }
    }
}

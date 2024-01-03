using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland
{
    public abstract class Controller : IController
    {
        public bool IsReady => _subsystemsActivator?.IsReady ?? default;
        public event Action<string, IController> OnReady;
        
        private readonly ISubsystemsActivator _subsystemsActivator;


        public Controller()
        {
            _subsystemsActivator = new SubsystemsActivator(this);
        }
        
        public virtual void Enable()
        {
            List<IController> controllers = _subsystemsActivator.SubsystemsControllers;
            CreateSubsystems(ref controllers);
            _subsystemsActivator.SetSubsystemsActivity(true);
        }

        public virtual void Enable(List<IController> subsystemsControllers)
        {
            _subsystemsActivator.SetSubsystemsActivity(true);
        }
        

        public void Disable()
        {
            _subsystemsActivator.SetSubsystemsActivity(false);
        }
        
        public void OnSystemReadyHandler(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                Debug.Log($"{this.GetType().FullName} subsystems are ready!");
            }
            else
            {
                Debug.Log($"<color=red> {this.GetType().FullName} System are not ready! </color>");
                Debug.LogError(errorMessage);
            }
            OnReady?.Invoke(errorMessage, this);
        }

        protected abstract void CreateSubsystems(ref List<IController> subsystemsControllers);
    }
}
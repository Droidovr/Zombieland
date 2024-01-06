using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland
{
    public abstract class Controller : IController
    {
        public bool IsActive { get; private set; }
        public event Action<string, IController> OnReady;

        protected readonly IController _parentController;
        private readonly ISubsystemsActivator _subsystemsActivator;
        private readonly IDependencyTracker _dependencyTracker;

/// <summary>
/// 
/// </summary>
/// <param name="parentController"></param>
/// <param name="requiredControllers">list of links to controllers of other modules required to run this system.</param>
        public Controller(IController parentController, List<IController> requiredControllers)
        {
            _parentController = parentController;
            _dependencyTracker = new DependencyTracker(this, requiredControllers);
            _subsystemsActivator = new SubsystemsActivator(this);
        }

        public virtual void Enable()
        {
            _dependencyTracker.OnReady += OnDependencysReadyHandler;
            _dependencyTracker.Init();
        }

        public virtual void Enable(List<IController> subsystemsControllers)
        {
            _subsystemsActivator.SetSubsystemsActivity(true);
        }

        public virtual void Disable()
        {
            _dependencyTracker.OnReady -= OnDependencysReadyHandler;
            _subsystemsActivator.OnReady -= OnSystemReadyHandler;
            
            _subsystemsActivator.OnReady += OnSystemReadyHandler;
            _subsystemsActivator.SetSubsystemsActivity(false);
        }


        /// <summary>
        /// If necessary, add the creation of helper scripts to this method.
        /// </summary>
        protected abstract void CreateHelpersScripts();

        /// <summary>
        /// Create subsystem controllers in this method and add them to the list.
        /// </summary>
        /// <param name="subsystemsControllers">reference list into which newly created subsystem controllers must be entered.</param>
        protected abstract void CreateSubsystems(ref List<IController> subsystemsControllers);


        private void OnDependencysReadyHandler(string errorMessage)
        {
            _dependencyTracker.OnReady -= OnDependencysReadyHandler;
            if (string.IsNullOrEmpty(errorMessage))
            {
                CreateHelpersScripts();
                List<IController> controllers = _subsystemsActivator.SubsystemsControllers;
                CreateSubsystems(ref controllers);
                _subsystemsActivator.OnReady += OnSystemReadyHandler;
                _subsystemsActivator.SetSubsystemsActivity(true);
            }
            else
            {
                OnReady?.Invoke(errorMessage, this);
            }
        }

        private void OnSystemReadyHandler(string errorMessage)
        {
            _subsystemsActivator.OnReady -= OnSystemReadyHandler;
            if (string.IsNullOrEmpty(errorMessage))
            {
                Debug.Log($"{this.GetType().FullName} subsystems are ready!");
            }
            else
            {
                Debug.Log($"<color=red> {this.GetType().FullName} System are not ready! </color>");
                Debug.LogError(errorMessage);
            }

            IsActive = _subsystemsActivator.IsActive;
            OnReady?.Invoke(errorMessage, this);
        }
    }
}
using System;
using UnityEngine;

namespace Zombieland.RootModule
{
    public interface IController
    {
        bool IsActive { get; }
        /// <summary>
        /// arg0 - ERROR message or empty string.
        /// arg1 - this controller.
        /// </summary>
        event Action<string, IController> OnReady;

        void Initialize<T>(T parentController);
        void Disable();
    }
}
using System;
using UnityEngine;
using Zombieland.RootModule;

public class TestCharacterController : IController, ITestCharacterDataController
{
    public bool IsActive => throw new NotImplementedException();

    public event Action<string, IController> OnReady;

    public void Disable()
    {
        throw new NotImplementedException();
    }

    public void Initialize<T>(T parentController)
    {
        throw new NotImplementedException();
    }
}

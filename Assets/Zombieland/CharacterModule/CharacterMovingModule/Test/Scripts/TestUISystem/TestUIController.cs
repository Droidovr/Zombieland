using System;
using UnityEngine;
using Zombieland.CharacterModule.CharacterMovingModule;
using Zombieland.RootModule;

public class TestUIController : MonoBehaviour, IController, ITestUIController
{
    public bool IsActive { get; private set; }

    public event Action<string, IController> OnReady;

    private TestInput _testInput;
    private GameObject _prefab;
    private string _prefabName = "Input";
    private GameObject _inputGameObject;

    public void Disable()
    {
        SetSystemsActivity(false);
    }

    public void Initialize<T>(T parentController)
    {
        _prefab = Resources.Load<GameObject>(_prefabName);

        _inputGameObject = Instantiate(_prefab);

        _testInput = _inputGameObject.GetComponent<TestInput>();

        if (_inputGameObject != null)
        {
            SetSystemsActivity(true);
        }
    }

    public Vector2 GetVectorInput()
    {
        return _testInput.InputVectorMove;
    }

    private void SetSystemsActivity(bool isActive)
    {
        IsActive = isActive;
        OnReady?.Invoke(String.Empty, this);
    }
}

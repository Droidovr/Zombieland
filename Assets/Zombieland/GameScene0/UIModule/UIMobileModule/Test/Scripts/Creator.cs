using UnityEngine;
using Zombieland.GameScene0.UIModule;


public class Creator
{
    public InputMobileManager InputSystem => _inputSystem;

    private InputMobileManager _inputSystem;


    public void Create()
    {
        var _prefab = Resources.Load<GameObject>("Joystick");

        Debug.Log("OK!!!!!!!!");

        Canvas canvas = GameObject.FindWithTag("InputSystem").GetComponent<Canvas>();

        Debug.Log("3333333333333");
        GameObject _inputSystemGameobject = GameObject.Instantiate(_prefab, canvas.transform);

        Debug.Log("444444444");
        _inputSystem = _inputSystemGameobject.GetComponent<InputMobileManager>();
    }
}

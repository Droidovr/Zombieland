using System.Collections.Generic;
using UnityEngine;
using Zombieland;
using Zombieland.GameScene0.NPCManagerModule;

public class TestStartPoint : MonoBehaviour
{
    public Transform CharacterTransform;
    
    private IController Controller;
    private readonly List<IController> ControllersList = new List<IController>();
    private NPCManagerController NPCManagerController;


    private void Awake()
    {
        NPCManagerController = new NPCManagerController(Controller, ControllersList);
    }

    void Start()
    {
        NPCManagerController.CharacterTransform = CharacterTransform;
    }
}

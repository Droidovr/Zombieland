using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland;
using Zombieland.GameScene0.NPCModule;

public class TestStartPoint : MonoBehaviour
{
    public Transform TargetTransform;
    
    private IController Controller;
    private readonly List<IController> ControllersList = new List<IController>();
    private INPCController NPCController;


    private void Awake()
    {
        NPCController = new NPCController(Controller, ControllersList);
    }

    void Start()
    {
        NPCController.MovingController.FollowTarget(TargetTransform, 1);
    }
}

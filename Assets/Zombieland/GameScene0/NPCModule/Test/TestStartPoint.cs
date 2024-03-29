using System.Collections.Generic;
using UnityEngine;
using Zombieland;
using Zombieland.GameScene0.NPCModule;

public class TestStartPoint : MonoBehaviour
{
    private IController Controller;
    private List<IController> ControllersList = new List<IController>();
    private INPCController NPCController;
    
    void Start()
    {
        NPCController = new NPCController(Controller, ControllersList);
    }
}

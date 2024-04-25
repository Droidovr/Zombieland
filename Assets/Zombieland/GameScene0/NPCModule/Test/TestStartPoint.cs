using System.Collections.Generic;
using UnityEngine;
using Zombieland;
using Zombieland.GameScene0.NPCManagerModule;

public class TestStartPoint : MonoBehaviour
{
    public Transform CharacterTransform;
    
    private IController Controller;
    private NpcManagerController NPCManagerController;

    void Start()
    {
        NPCManagerController = new NpcManagerController(Controller, null, CharacterTransform);
    }
}

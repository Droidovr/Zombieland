using UnityEngine;
using Zombieland;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.NPCModule.NPCAimingController;

public class Aiming
{
    private INPCAimingController _nPCAimingController;
    private ICharacterController _characterController;


    public Aiming(INPCAimingController nPCAimingController)
    {
        _nPCAimingController = nPCAimingController;
        _nPCAimingController.NPCController.NPCAwarenessController.OnDetectCharacter += DetectCharacterHandler;
    }

    public Transform GetTarget()
    { 
        if (_characterController != null) 
        {
            return _characterController.VisualBodyController.CharacterInScene.GetComponent<Transform>();
        }

        return null;
    }

    private void DetectCharacterHandler(IController controller, bool isDetect)
    {
        if (isDetect)
        {
            _characterController = controller as ICharacterController;
        }
        else
        {
            _characterController = null;
        }
    }
}

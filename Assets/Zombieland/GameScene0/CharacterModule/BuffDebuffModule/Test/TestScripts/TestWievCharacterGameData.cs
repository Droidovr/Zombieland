using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.RootModule;

public class TestWievCharacterGameData : MonoBehaviour
{
    private ICharacterController _characterController;

    private CharacterData _characterData;
    private TextMeshProUGUI _textMeshProComponent;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GameObject.Find("Character0Ragdoll(Clone)").GetComponent<ICharacterController>();
        _textMeshProComponent = GetComponent<TextMeshProUGUI>();

        _characterData = _characterController.CharacterDataController.CharacterData;

        Debug.Log("<color=red>" + _characterData.ToString() + "</color>");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_characterData != null)
        {
            string s = "";
            s += "MaxMovingSpeed: " + _characterData.MaxMovingSpeed;
            s += "\nDesignMovingSpeed: " + _characterData.DesignMovingSpeed;
            s += "\nMaxRotationSpeed: " + _characterData.MaxRotationSpeed;
            s += "\nDesignRotationSpeed: " + _characterData.DesignRotationSpeed;
            s = "\nHP: " + _characterData.HP;
            s += "\nHPMax: " + _characterData.HPMax;
            s += "\nHPDefault: " + _characterData.HPDefault;

            _textMeshProComponent.text = s;
        }
    }
}

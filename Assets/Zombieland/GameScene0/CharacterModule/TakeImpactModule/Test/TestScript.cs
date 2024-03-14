using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class TestScript : MonoBehaviour
{

    private bool _inArea = false;
    private Coroutine _coroutine;
    private CharacterController characterController;

    private void OnTriggerEnter(Collider other)
    {
        var characterController = other.gameObject.GetComponent<CharacterController>();
        if (characterController)
        {
            Debug.Log("player enter");
            _inArea = true;
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(periodicalDamage());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {
            Debug.Log("player exit");
            _inArea = false;
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }

    IEnumerator periodicalDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (_inArea)
            {
                Debug.Log("10 damage");
                characterController.
            }
        }
    }

}

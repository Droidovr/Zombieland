using System.Collections;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.CharacterModule.SensorModule;

public class TestScript : MonoBehaviour
{

    private bool _inArea = false;
    private Coroutine _coroutine;
    private ImpactDetectionSensor _sensor;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player enter");
        if (other.gameObject.GetComponent<ImpactDetectionSensor>())
        {
         
           
            _sensor = other.gameObject.GetComponent<ImpactDetectionSensor>();
            _inArea = true;
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(periodicalDamage());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("player exit");
        if (other.gameObject.GetComponent<ImpactDetectionSensor>())
        {
           
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
                _sensor.GetImpactableObject().ApplyImpact(new DirectImpactData() { AbsoluteValue = 10 }); // for full test add log or listener to HP in CaracterData
                Debug.Log("player take damage");
            }
        }
    }

}

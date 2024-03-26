using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodstainsTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WalkingBloodstains>() != null)
        {
            other.GetComponent<WalkingBloodstains>().activeSteps = 15;
        }
    }
}

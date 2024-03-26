using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBloodstains : MonoBehaviour
{
    [SerializeField] private GameObject _footprint;
    [SerializeField] private Transform _rightLegTransform;
    [SerializeField] private Transform _leftLegTransform;

    public int activeSteps = 0;

    public void LeftStepFootprintRender()
    {
        if (activeSteps <= 0)
        {
            return;
        }
        Instantiate(_footprint, _leftLegTransform.position, Quaternion.Euler(90, 0, 200), null);
        activeSteps--;
    }

    public void RightStepFootprintRender()
    {
        if (activeSteps <= 0)
        {
            return;
        }
        Instantiate(_footprint, _rightLegTransform.position, Quaternion.Euler(90, 0, 200), null);
        activeSteps--;
    }
}

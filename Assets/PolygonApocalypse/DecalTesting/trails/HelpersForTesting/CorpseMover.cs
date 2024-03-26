using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseMover : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;

    [SerializeField] private float speed;
    private int currentMoveIndex = 0;
    private Transform currentDestination;

    private void Start()
    {
        currentDestination = movePoints[currentMoveIndex];
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentDestination.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentDestination.position) < .05f)
        {
            currentMoveIndex++;
            currentDestination = movePoints[currentMoveIndex];
            transform.LookAt(currentDestination.position);
        }
    }
}

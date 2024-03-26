using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailTesting : MonoBehaviour
{
    [SerializeField] private GameObject particlesTrail;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float trailStopMovingDelay;

    private float spawnCooldownRemaining;
    private float _extractionTime;
    private float _currentExtractionTime;

    private readonly Queue<GameObject> _trailObjectList = new Queue<GameObject>();

    private void Start()
    {
        _extractionTime = spawnCooldown + trailStopMovingDelay;
    }

    private void Update()
    {
        if (spawnCooldownRemaining >= spawnCooldown)
        {
            var trail = Instantiate(particlesTrail, transform);
            _trailObjectList.Enqueue(trail);
            spawnCooldownRemaining = 0;
        }

        if (_currentExtractionTime >= _extractionTime)
        {
            var trail = _trailObjectList.Dequeue();
            trail.transform.parent = null;
            _extractionTime = spawnCooldown;
            _currentExtractionTime = 0;
        }

        spawnCooldownRemaining += Time.deltaTime;
        _currentExtractionTime += Time.deltaTime;
    }
}

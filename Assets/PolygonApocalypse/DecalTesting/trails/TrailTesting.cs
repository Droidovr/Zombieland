using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailTesting : MonoBehaviour
{
    [SerializeField] private GameObject particlesTrail;
    [SerializeField] private float spawnCooldown;

    private float spawnCooldownRemaining;
    private GameObject currentTrail;

    private void Update()
    {
        if (spawnCooldownRemaining >= spawnCooldown)
        {
            if (currentTrail != null)
            {
                currentTrail.transform.parent = null;
            }
            currentTrail = Instantiate(particlesTrail, transform);
            spawnCooldownRemaining = 0;
        }

        spawnCooldownRemaining += Time.deltaTime;

    }
}

using UnityEngine;

public class GeneratorRotation : MonoBehaviour
{
    public GameObject GeneratorPrefab;
    public float GeneratorRotateSpeed;


    void Update()
    {
        RotateTurretBarrel();
    }

    private void RotateTurretBarrel()
    {
        float rotationSpeed = GeneratorRotateSpeed * Time.deltaTime;
        GeneratorPrefab.transform.Rotate(0, 0, rotationSpeed);
    }
}

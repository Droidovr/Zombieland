using UnityEngine;

public class RotationGameObject : MonoBehaviour
{
    [SerializeField] private GameObject GeneratorPrefab;
    [SerializeField] private float RotateSpeed;

    [SerializeField] private bool isRotateAxisX;
    [SerializeField] private bool isRotateAxisY;
    [SerializeField] private bool isRotateAxisZ;

    void Update()
    {
        RotateGameObject();
    }

    private void RotateGameObject()
    {
        float rotationSpeed = RotateSpeed * Time.deltaTime;
        if (isRotateAxisX)
        {
            GeneratorPrefab.transform.Rotate(rotationSpeed, 0, 0);
        }
        if (isRotateAxisY)
        {
            GeneratorPrefab.transform.Rotate(0, rotationSpeed, 0);
        }
        if (isRotateAxisZ)
        {
            GeneratorPrefab.transform.Rotate(0, 0, rotationSpeed);
        }

    }
}

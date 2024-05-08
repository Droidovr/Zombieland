using UnityEngine;

public class BlinkOfLight : MonoBehaviour
{
    [SerializeField] private GameObject light;
    [SerializeField] private float flickerMin;
    [SerializeField] private float flickerMax;

    void Awake() => LightOn();  
    
    void LightOn()
    {
        light.SetActive(true);
        Invoke("LightOff", Random.Range(flickerMin, flickerMax));
    }

    void LightOff()
    {
        light.SetActive(false);
        Invoke("LightOn", Random.Range(flickerMin, flickerMax));
    }
}

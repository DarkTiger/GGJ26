using UnityEngine;

public class PlantTest : MonoBehaviour
{
    Plant plant;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plant = GetComponent<Plant>();
        plant.IsWet = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

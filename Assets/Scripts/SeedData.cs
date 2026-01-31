using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SeedData", menuName = "Data/SeedData")]
public class SeedData : ScriptableObject
{
    public GameObject SeedPrefab;
    public GameObject PlantPrefab;
    public Sprite Icon;
    public int Cost;
}

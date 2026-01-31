using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SeedData", menuName = "Data/SeedData")]
public class SeedData : ScriptableObject
{
    public GameObject prefab;
    public Sprite icon;
    public int cost;
}

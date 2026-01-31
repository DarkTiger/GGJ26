using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Data/Ingredient")]
public class SO_Ingredient : ScriptableObject
{
    public ItemStatus startStatus = ItemStatus.BASE;
    [Space(10)]
    public Sprite baseSprite;
    public Sprite cookedSprite;
    public Sprite powderSprite;
}

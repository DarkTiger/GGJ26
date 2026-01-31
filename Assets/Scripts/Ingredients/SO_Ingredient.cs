using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Data/Ingredient")]
public class SO_Ingredient : ScriptableObject
{
    public IngredientStatus startStatus = IngredientStatus.BASE;
    [HideInInspector]   public IngredientStatus currentStatus = IngredientStatus.BASE;
    [ColorUsage(showAlpha:false)]   public Color workedColor = Color.white;
    [Space(10)]
    public Sprite baseSprite;
    public Sprite cookedSprite;
    public Sprite powderSprite;
}
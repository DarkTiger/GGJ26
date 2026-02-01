using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Data/Ingredient")]
public class SO_Ingredient : ScriptableObject
{
    public IngredientStatus startStatus = IngredientStatus.BASE;
    [ColorUsage(showAlpha:false)]   public Color workedColor = Color.white;
    [Space(10)]
    public Sprite baseSprite;
    public Sprite cookedSprite;
    public Sprite powderSprite;
    [Space(10)]
    public Sprite cauldronSprite;
    public Sprite mortarSprite;

    public Sprite GetSprite(IngredientStatus status)
    {
        Sprite targetSprite;
        switch (status)
        {
            case IngredientStatus.BASE:
                targetSprite = baseSprite;
            break;
            case IngredientStatus.COOKED:
                targetSprite = cookedSprite;
            break;
            case IngredientStatus.POWDER:
                targetSprite = powderSprite;
            break;
            default:
                targetSprite = baseSprite;
            break;
        }

        if(targetSprite == null)
            return baseSprite;

        return targetSprite;
    }
}
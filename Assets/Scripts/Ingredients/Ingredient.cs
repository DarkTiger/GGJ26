using UnityEngine;

public enum IngredientState
{
    BASE = 0,
    COOKED = 10,
    POWDER = 20,
}

public class Ingredient : MonoBehaviour
{
    public Sprite baseSprite, cookedSprite, powderSprite;

    IngredientState currentState = IngredientState.BASE;
    public void Use()
    { print("Used: " + this.GetType()); }

    public void ChangeState(IngredientState state)
    {
        currentState = state;
    }
}

using UnityEngine;

public class Ingredient : Item
{
    public Sprite baseSprite, cookedSprite, powderSprite;

    ItemState currentState = ItemState.BASE;
    public void Use()
    { print("Used: " + this.GetType()); }

    public void ChangeState(ItemState state)
    {
        currentState = state;
    }
}
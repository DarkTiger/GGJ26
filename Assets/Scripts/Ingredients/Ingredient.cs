using UnityEngine;

public class Ingredient : Item
{
    SpriteRenderer spriteRenderer;
    public Sprite baseSprite, cookedSprite, powderSprite;

    ItemStatus currentState = ItemStatus.BASE;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Use()
    { print("Used: " + this.GetType()); }

    public void ChangeState(ItemStatus state)
    {
        currentState = state;
    }
}
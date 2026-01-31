using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public SO_Ingredient data;

    SpriteRenderer spriteRenderer;
    IngredientStatus currentState = IngredientStatus.BASE;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeState(IngredientStatus state)
    {
        currentState = state;
    }
}
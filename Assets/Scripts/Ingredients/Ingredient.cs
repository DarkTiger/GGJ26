using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public SO_Ingredient data;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeState(IngredientStatus state)
    {
        data.currentStatus = state;
    }
}
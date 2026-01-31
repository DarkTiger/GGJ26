using UnityEngine;

public class Cauldron : Machine
{
    protected override IngredientState finalState => IngredientState.COOKED;
}

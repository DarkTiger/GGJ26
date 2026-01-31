using UnityEngine;

public class Mortar : Machine
{
    protected override IngredientState finalState => IngredientState.POWDER;
}

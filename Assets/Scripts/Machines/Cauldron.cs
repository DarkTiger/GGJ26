using UnityEngine;

public class Cauldron : Machine
{
    public override IngredientStatus finalState => IngredientStatus.COOKED;
}

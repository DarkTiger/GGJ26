using UnityEngine;

public class Cauldron : Machine
{
    protected override ItemState finalState => ItemState.COOKED;
}

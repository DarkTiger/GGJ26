using UnityEngine;

public class Cauldron : Machine
{
    protected override ItemStatus finalState => ItemStatus.COOKED;
}

using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Upgradable : Interactable
{
    [SerializeField] GameObject popup;
    [SerializeField] Sprite item;
    [SerializeField] MonoBehaviour itemScript;
    
    public override void OnInteract(Player player)
    {
        base.OnInteract(player);

    }
    public override void OnDeInteract(Player player)
    {

    }

    public override void OnEntering(Player player)
    {
        
    }

    public override void OnExit(Player player)
    {
        
    }
}

using UnityEngine;

public class Interactable : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Player InteractedBy { get; set; }
    public virtual void OnInteract(Player player) { }
    public virtual void OnDeInteract(Player player) { }
    public virtual void OnEntering(Player player) { }
    public virtual void OnExit(Player player) { }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
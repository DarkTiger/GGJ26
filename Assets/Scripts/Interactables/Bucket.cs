using UnityEngine;

public class Bucket : Item
{
    [SerializeField] Sprite fullSprite;
    [SerializeField] Sprite emptySprite;

    public bool IsFull { get; private set; }

    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Use(Player player)
    {
        SetFull(false);
        base.Use(player);
    }

    public override void OnDeInteract(Player player)
    {
        if (player.CandidateInteractable is CampSlot || player.CandidateInteractable is Well) return;

        player.CurrentItem = null;

        base.OnDeInteract(player);
    }

    public void SetFull(bool isFull)
    {
        if (UseClip)
        {
            AudioSource.PlayClipAtPoint(UseClip, Camera.main.transform.position, 0.5f);
        }

        IsFull = isFull;
        spriteRenderer.sprite = isFull? fullSprite : emptySprite;
    }
}
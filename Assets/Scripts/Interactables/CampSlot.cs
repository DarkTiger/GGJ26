using UnityEngine;

public class CampSlot : Interactable
{
    [SerializeField] Sprite drySprite;
    [SerializeField] Sprite wetSprite;
    
    public bool IsWet { get; private set; }
    public SeedData CurrentSeed { get; private set; }
        
    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void SetWet(bool isWet)
    {
        IsWet = isWet;
        spriteRenderer.sprite = isWet? wetSprite : drySprite;
    }

    public override void OnInteract(Player player)
    {
        if (player.CandidateInteractable == this && player.CurrentItem)
        {
            if (player.CurrentItem is Bucket)
            {
                if ((player.CurrentItem as Bucket).IsFull)
                {
                    player.CurrentItem.Use(player);
                    SetWet(true);
                }
            }
            else if (player.CurrentItem is Seed)
            {
                (player.CurrentItem as Seed).SetSlotPos(transform.position);
                player.CurrentItem.Use(player);               
            }
        }
    }
}

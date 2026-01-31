using UnityEngine;

public class CampSlot : Interactable
{
    [SerializeField] Sprite drySprite;
    [SerializeField] Sprite wetSprite;
    [SerializeField] float timeToPlantGrow;

    public bool IsWet { get; private set; }
    public SeedData CurrentSeed { get; private set; }
    public int GrowLevel { get; private set; } = 0;

    const float GROW_TIME = 0f;
    
    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!CurrentSeed || !IsWet || CurrentSeed && GrowLevel >= 3) return;

        for (int i = 0; i < 3; i++)
        {
            if (timeToPlantGrow * (GrowLevel + 1) <= GROW_TIME)
            {
                // GROW PLANT LOGIC
                GrowLevel++;
            }
        }
    }

    void SetWet(bool isWet)
    {
        IsWet = isWet;
        spriteRenderer.sprite = isWet? wetSprite : drySprite;
    }

    public override void OnInteract(Player player)
    {
        if (player.CandidateInteractable == this && player.CurrentItem && player.CurrentItem is Bucket)
        {
            if ((player.CurrentItem as Bucket).IsFull)
            {
                player.CurrentItem.Use(player);
                SetWet(true);
            }
        }
    }
}

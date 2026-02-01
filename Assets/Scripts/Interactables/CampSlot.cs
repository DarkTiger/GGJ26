using System;
using UnityEngine;

public class CampSlot : Interactable
{
    [SerializeField] AudioClip cultivateClip;
    [SerializeField] Sprite drySprite;
    [SerializeField] Sprite wetSprite;
    
    public bool IsWet { get; private set; }
    public Seed CurrentSeed { get; private set; }    
    public Plant CurrentPlant { get; set; }

    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void SetWet(bool isWet)
    {
        IsWet = isWet;
        spriteRenderer.sprite = isWet? wetSprite : drySprite;

        if (CurrentSeed)
        {
            CurrentSeed.SetWet(isWet);
            CurrentSeed.Plant.IsWet = isWet;
        }
    }

    public override void OnInteract(Player player)
    {
        if (player.CandidateInteractable == this && player.CurrentItem)
        {
            if (player.CurrentItem is Bucket)
            {
                if ((player.CurrentItem as Bucket).IsFull)
                {
                    SetWet(true);
                    if (CurrentPlant)
                    {
                        CurrentPlant.IsWet = true;
                    }
                    player.CurrentItem.Use(player);
                }
            }
            else if (player.CurrentItem is Seed)
            {
                if (CurrentSeed) return;

                CurrentSeed = player.CurrentItem as Seed;
                CurrentSeed.SetWet(IsWet);
                CurrentSeed.CampSlot = this;
                (player.CurrentItem as Seed).SetSlotPos(transform.position);
                player.CurrentItem.Use(player);
                AudioSource.PlayClipAtPoint(cultivateClip, Camera.main.transform.position, 0.5f);
            }
        }
    }
}

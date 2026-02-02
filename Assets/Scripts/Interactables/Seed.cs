using UnityEngine;

public class Seed : Item
{
    public Plant Plant { get; private set; } 
    public CampSlot CampSlot { get; set; }

    SeedData seedData;
    Vector3 slotPos;
    bool isWet;

    public override void Use(Player player)
    {
        FarmPlant();
        base.Use(player);
    }

    public void SetSeedData(SeedData seedData)
    {
        this.seedData = seedData;
    }

    public void SetSlotPos(Vector3 slotPos)
    {
        this.slotPos = slotPos;
    }

    public void SetWet(bool isWet)
    {
        this.isWet = isWet;
    }

    void FarmPlant()
    {
        Plant = Instantiate(seedData.PlantPrefab, slotPos, Quaternion.identity).GetComponent<Plant>();
        Plant.isWet = isWet;
        CampSlot.CurrentPlant = Plant;
        Destroy(gameObject);
    }

    public override void OnDeInteract(Player player)
    {
        if (player.CandidateInteractable is CampSlot) return;

        player.CurrentItem = null;
        base.OnDeInteract(player);
    }
}

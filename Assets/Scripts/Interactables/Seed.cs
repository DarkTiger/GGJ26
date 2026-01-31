using UnityEngine;

public class Seed : Item
{
    SeedData seedData;
    Vector3 slotPos;

    public override void Use(Player player)
    {
        Plant();
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

    void Plant()
    {
        Instantiate(seedData.PlantPrefab, slotPos, Quaternion.identity);
    }
}

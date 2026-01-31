using UnityEngine;

public class Plant : Item
{
    public bool IsWet { get; set; }
    public int GrowLevel { get; private set; } = 0;
    public GameObject IngredientPrefab;

    float growingTime;
    const float GROW_TIME = 30f;


    private void Update()
    {
        if (!IsWet || GrowLevel >= 2) return;

        growingTime += Time.deltaTime;

        for (int i = 0; i < 3; i++)
        {
            if (GROW_TIME * (GrowLevel + 1) <= growingTime)
            {
                AddGrowLevel();
            }
        }
    }

    public void AddGrowLevel()
    {
        GrowLevel++;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(GrowLevel == 1);
        transform.GetChild(2).gameObject.SetActive(GrowLevel == 2);

        if (GrowLevel == 2)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }

    public override void OnInteract(Player player)
    {
        if(player.CurrentItem != null)
            return;

        GameObject ingredient = Instantiate(IngredientPrefab);
        ingredient.transform.position = transform.position;
        
        ingredient.GetComponent<Interactable>().OnInteract(player);

        Destroy(gameObject);
    }
}
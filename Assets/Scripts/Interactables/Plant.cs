using UnityEngine;

public class Plant : Item
{
    public bool IsWet { get; set; }
    public int GrowLevel { get; private set; } = 0;
    public GameObject IngredientPrefab;
    public SO_Ingredient finalIngredient;

    float growingTime;
    public float GROW_TIME = 20f;

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
        base.OnInteract(player);

        print("interacting with plant");
        if(GrowLevel == 2)
        {
            GameObject ingredient = Instantiate(IngredientPrefab);
            ingredient.GetComponent<Ingredient>()?.UpdateSprite();
            ingredient.transform.position = transform.position;
            ingredient.GetComponent<Interactable>().OnInteract(player);
            Item it = ingredient.GetComponent<Item>();
            player.CurrentInteractable = it;
            player.CurrentItem = it;
            Destroy(gameObject);
        }
    }
}
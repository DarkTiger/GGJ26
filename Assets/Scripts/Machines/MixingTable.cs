using System.Collections.Generic;
using UnityEngine;

public class MixingTable : Machine
{
    [SerializeField]    List<SO_Ingredient> ingredients;

    public override void OnInteract(Player player)
    {
        print("Interacting with: " + name);

        if(player.CurrentItem is Ingredient)

        if(ingredients == null)
        {
            if(player.CurrentItem != null)
            {
                ingredients.Add((player.CurrentItem as Ingredient).data);
                player.CurrentItem = null;
                player.CurrentInteractable = null;
            }
            else
            {
                // avvisare player?
            }
        }
        else
        {
            if (player.CurrentItem != null)
            {
                ingredients.Add((player.CurrentItem as Ingredient).data);
                    List<Recipe> recipeList = Recipe.CheckRecipeAvailability(GameManager.Instance.availableRecipe, ingredients);
                    
                    if(recipeList.Count == 1)
                    {

                    }
            }
            else
            {

            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("WAIT") && itemInside == null && player.CandidateInteractable == this)
        {
            if (player.CurrentItem?.gameObject.GetComponent<Ingredient>() == null)
                return;

            print("Putting item in mixing");
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FINISH") && itemInside != null)
        {
            if (player.CurrentItem != null)
                return;

            print("Removing item in cauldron");
        }
    }
}

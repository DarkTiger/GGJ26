using System.Collections.Generic;
using UnityEngine;

public class MixingTable : Machine
{
    [SerializeField] List<ProcessedIngredient> ingredients;
    [SerializeField] GameObject maskPrefab;
    [SerializeField] Sprite workingSprite;

    protected override void ReadyBehaviour()
    {
        StartCoroutine(InteractionDelay(workTime));

        AddIngredient();
        UseWorkingSprite();
        state = MachineState.WORKING;
    }

    protected override void WorkingBehaviour()
    {
        AddIngredient();
        StartCoroutine(InteractionDelay(workTime));
    }

    protected override void FinishedBehaviour()
    {
        if (lastInteractionPlayer.CurrentItem == null)
        {
            ingredients.Clear();
            popUp.Hide();
            GiveItemToPlayer();
            state = MachineState.READY;
        }
    }

    public override void AfterInteractionDelay()
    {
        switch (state)
        {
            case MachineState.LOCKED:
                break;
            case MachineState.READY:
                popUp.Hide();
                animator.SetTrigger("End");
                spriteRenderer.sprite = initialSprite;
                break;
            case MachineState.WORKING:
                animator.SetTrigger("End");

                Recipe recipe = Recipe.GetRecipeFromIngredients(ingredients, out bool partial);
                if (recipe == null)
                {
                    ingredients.Clear();
                    spriteRenderer.sprite = initialSprite;
                    state = MachineState.READY;
                }
                else if (!partial)
                {
                    CreateMask(recipe);
                    popUp.UpdateFG(recipe.sprite);
                    popUp.Show();
                    state = MachineState.FINISHED;
                }
                break;
            case MachineState.FINISHED:
                animator.SetTrigger("End");
                break;
        }
    }

    void AddIngredient()
    {
        Ingredient inputIngredient = lastInteractionPlayer.CurrentItem?.gameObject.GetComponent<Ingredient>();

        if (inputIngredient == null)
            return;

        animator.SetTrigger("Start");

        ProcessedIngredient newProIng;
        newProIng.ingredient = inputIngredient.data;
        newProIng.status = inputIngredient.status;
        ingredients.Add(newProIng);
        lastInteractionPlayer.CurrentItem.Release();
        Destroy(inputIngredient.gameObject);
    }

    void CreateMask(Recipe recipe)
    {
        GameObject maskObj = Instantiate(maskPrefab);
        itemInside = maskObj;
        itemInside.GetComponent<Collider2D>().enabled = false;

        maskObj.transform.parent = transform;
        maskObj.transform.position = transform.position;
        maskObj.gameObject.SetActive(false);
        maskObj.GetComponent<Mask>().recipe = recipe;
        maskObj.GetComponent<Mask>().UpdateSprite();
    }

    protected override void UseWorkingSprite()
    {
        if (workingSprite == null)
            return;

        spriteRenderer.sprite = workingSprite;
    }
}

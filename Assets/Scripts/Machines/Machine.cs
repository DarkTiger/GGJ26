using System.Collections;
using UnityEngine;

public class Machine : Interactable
{
    [Space(10)]
    [SerializeField] protected float workTime = 4f;
    public GameObject itemInside = null;
    protected PopUp popUp;
    protected SpriteRenderer spriteRenderer;
    protected Sprite initialSprite;
    protected virtual IngredientStatus finalState { get; }
    protected Animator animator;
    protected Player lastInteractionPlayer;
    protected MachineState state = MachineState.READY;
    protected bool isInteractable = true;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialSprite = spriteRenderer.sprite;
        popUp = GetComponentInChildren<PopUp>();
        popUp.Hide();
    }

    public override void OnInteract(Player player)
    {
        if (!isInteractable)
            return;

        lastInteractionPlayer = player;

        switch (state)
        {
            case MachineState.LOCKED:
                LockedBehaviour();
                break;
            case MachineState.READY:
                ReadyBehaviour();
                break;
            case MachineState.WORKING:
                WorkingBehaviour();
                break;
            case MachineState.FINISHED:
                FinishedBehaviour();
                break;
        }
    }

    protected virtual void LockedBehaviour() { }
    protected virtual void ReadyBehaviour()
    {
        StartCoroutine(InteractionDelay(workTime));
        Ingredient inputIngredient = lastInteractionPlayer.CurrentItem?.gameObject.GetComponent<Ingredient>();

        if (inputIngredient == null)
            return;
        if (inputIngredient.status != IngredientStatus.BASE)
            return;

        state = MachineState.WORKING;
        IngestIngredient();
        UseWorkingSprite();
    }

    protected virtual void FinishedBehaviour()
    {
        if (lastInteractionPlayer.CurrentItem != null)
            return;

        GiveItemToPlayer();
        state = MachineState.READY;
    }

    protected virtual void WorkingBehaviour() { }

    protected virtual void UseWorkingSprite() { }

    public virtual void AfterInteractionDelay()
    {
        switch (state)
        {
            case MachineState.LOCKED:
                break;
            case MachineState.READY:
                break;
            case MachineState.WORKING:
                animator.SetTrigger("End");
                popUp.Show();
                Ingredient ing = itemInside.GetComponent<Ingredient>();
                ing.ChangeState(finalState);
                popUp.UpdateFG(ing.data.GetSprite(finalState));
                state = MachineState.FINISHED;
                break;
            case MachineState.FINISHED:
                break;
        }
    }

    protected void GiveItemToPlayer()
    {
        itemInside.transform.SetParent(lastInteractionPlayer.transform, true);
        itemInside.SetActive(true);

        Item it = itemInside.gameObject.GetComponent<Item>();
        (it as Ingredient)?.UpdateSprite();

        it.Grab(lastInteractionPlayer);
        lastInteractionPlayer.CurrentItem = it;
        lastInteractionPlayer.CurrentInteractable = it;
        itemInside = null;

        spriteRenderer.sprite = initialSprite;
        popUp.Hide();
    }

    protected void IngestIngredient()
    {
        itemInside = lastInteractionPlayer.CurrentItem.gameObject;
        itemInside.GetComponent<Collider2D>().enabled = false;
        itemInside.transform.SetParent(transform);
        itemInside.transform.position = transform.position;
        itemInside.SetActive(false);
        lastInteractionPlayer.CurrentItem.Release();
        animator.SetTrigger("Start");
    }

    protected IEnumerator InteractionDelay(float delay)
    {
        isInteractable = false;
        yield return new WaitForSeconds(delay);
        isInteractable = true;
        AfterInteractionDelay();
        yield return null;
    }
}
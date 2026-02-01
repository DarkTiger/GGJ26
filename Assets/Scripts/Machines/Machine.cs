using Unity.VisualScripting;
using UnityEngine;

public enum MachineState
{
    LOCKED = 0,
    WAIT = 10,
    WORKING = 20,
    FINISHED = 30,
}

public class Machine : Interactable
{
    [Space(10)]
    public float workTime = 4f;
    public GameObject itemInside = null;
    [HideInInspector] public PopUp popUp;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    protected Sprite initialSprite;
    [Space(10)]
    [HideInInspector] public float startTime;

    public virtual IngredientStatus finalState { get; }

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        popUp = GetComponentInChildren<PopUp>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        initialSprite = spriteRenderer.sprite;

        //BH_MachineBehaviour[] bh = animator.GetBehaviours<BH_MachineBehaviour>();
        //foreach (BH_MachineBehaviour mbh in bh)
        //{
        //    mbh.machine = this;
        //}
    }
    
    public override void OnInteract(Player player)
    {
        print("Interacting with: " + name);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("WAIT") && itemInside == null && player.CandidateInteractable == this)
        {
            Ingredient inputIngredient = player.CurrentItem?.gameObject.GetComponent<Ingredient>();

            if (inputIngredient == null)
                return;
            if (inputIngredient.status != IngredientStatus.BASE)
                return;

                print("Putting item in cauldron");
                IngestItem(player);
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("FINISH") && itemInside != null)
        {
            if (player.CurrentItem != null)
                return;

            print("Removing item from cauldron");
            GiveItem(player);
        }
    }

    protected void IngestItem(Player player, bool hide=true)
    {
        itemInside = player.CurrentItem.gameObject;
        itemInside.GetComponent<Collider2D>().enabled = false;
        player.CurrentItem.Release();
        player.CurrentItem = null;
        itemInside.transform.SetParent(transform, true);
        if(hide)
            itemInside.gameObject.SetActive(false);

        animator.Play("WORK");
        UseWorkingSprite();
    }

    protected virtual void UseWorkingSprite()
    {
    }
    
    public virtual void FinishWorking()
    {
        popUp.Show();
        Ingredient ing = itemInside.GetComponent<Ingredient>();
        popUp.UpdateFG(ing.data.GetSprite(IngredientStatus.COOKED));
    }

    protected void GiveItem(Player player)
    {
        itemInside.transform.SetParent(player.transform, true);
        itemInside.gameObject.SetActive(true);
        itemInside.gameObject.GetComponent<Ingredient>()?.ChangeState(finalState);

        Item it = itemInside.gameObject.GetComponent<Item>();
        (it as Ingredient)?.UpdateSprite();

        it.Grab(player);
        player.CurrentItem = it;
        player.CurrentInteractable = it;
        itemInside = null;

        animator.Play("WAIT");
        spriteRenderer.sprite = initialSprite;
        popUp.Hide();
    }
}
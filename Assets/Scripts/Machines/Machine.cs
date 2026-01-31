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
    [Space(10)]
    [HideInInspector] public float startTime;

    public virtual IngredientStatus finalState { get; }

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        BH_MachineBehaviour[] bh = animator.GetBehaviours<BH_MachineBehaviour>();
        foreach(BH_MachineBehaviour mbh in bh)
        {
            mbh.machine = this;
        }    
    }

    public override void OnInteract(Player player)
    {
        if(player.CurrentItem == null)
            return;
        
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("WAIT") && itemInside == null)
        {
            if (player.CurrentItem.GetComponent<Ingredient>() == null)
                return;

            itemInside = player.CurrentItem.gameObject;
            itemInside.transform.SetParent(transform, true);
            itemInside.gameObject.SetActive(false);
            animator.Play("WORK");
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("FINISH") && itemInside != null)
        {
            if (player.CurrentItem != null)
                return;

            itemInside.transform.SetParent(player.transform, true);
            itemInside.gameObject.SetActive(true);
            itemInside.GetComponent<Ingredient>().ChangeState(finalState);
            player.CurrentItem = itemInside.GetComponent<Item>();
            itemInside = null;
            animator.Play("WAIT");
        }
    }
}
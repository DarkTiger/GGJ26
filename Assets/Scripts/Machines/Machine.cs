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
    public float workTime = 4f;
    [Space(10)]

    [HideInInspector] public float startTime;

    // TEST
    [SerializeField] GameObject targetObj;
    [SerializeField] bool start = false;

    protected virtual ItemStatus finalState { get; }

    Animator animator;
    Ingredient ingredient = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
        BH_MachineBehaviour[] bh = animator.GetBehaviours<BH_MachineBehaviour>();
        foreach(BH_MachineBehaviour mbh in bh)
        {
            mbh.machine = this;
        }    
    }

    private void Update()
    {
        if(start)
        { 
            if(targetObj.TryGetComponent(out Ingredient ing))
            {
                ingredient = ing;
                OnInteract(null);
            }
            start = false;
        }
    }

    public override void OnInteract(Player player)
    {
        print("interacting");

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("WAIT"))
            animator.Play("WORK");
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("FINISH"))
            animator.Play("WAIT");
    }
}
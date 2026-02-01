using UnityEngine;

public class BH_Machine_Finish : BH_MachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetMachine(animator).FinishWorking();
    }
}
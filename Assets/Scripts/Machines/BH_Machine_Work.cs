using UnityEngine;

public class BH_Machine_Work : BH_MachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        machine = GetMachine(animator);
        machine.startTime = Time.time;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        machine = GetMachine(animator);
        if(Time.time - machine.startTime > machine.workTime)
        {
            animator.Play("FINISH");
        }
    }
}
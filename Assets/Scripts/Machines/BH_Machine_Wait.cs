using UnityEngine;
public class BH_Machine_Wait : BH_MachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        machine = GetMachine(animator);
        machine.popUp.Hide();
    }
}
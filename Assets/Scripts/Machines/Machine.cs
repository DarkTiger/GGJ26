using UnityEditor.UI;
using UnityEngine;

public enum MachineState
{
    LOCKED = 0,
    WAIT = 10,
    WORKING = 20,
    FINISHED = 30,
}

public class Machine : MonoBehaviour
{
    public float workTime = 4f;
    [Space(10)]
    public Sprite lockedSprite, normalSprite, workingSprite, finishSprite;

    // TEST
    [SerializeField] GameObject targetObj;
    [SerializeField] bool start = false;

    protected virtual IngredientState finalState { get; }

    [SerializeField] MachineState currentState = MachineState.WAIT;
    Ingredient ingredient = null;
    float startTime;

    public void StartWorking(Ingredient ing)
    {
        ingredient = ing;
        currentState = MachineState.WORKING;
        startTime = Time.time;
    }

    private void Update()
    {
        if(start)
        { 
            if(targetObj.TryGetComponent(out Ingredient ing))
            StartWorking(ing);
            start = false;
        }

        if (currentState == MachineState.WORKING)
        {
            if (Time.time - startTime > workTime)
            {
                currentState = MachineState.FINISHED;
                FinishWork();
            }
        }
    }

    void FinishWork()
    {
        ingredient.ChangeState(finalState);
    }
}

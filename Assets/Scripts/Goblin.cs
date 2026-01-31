using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Goblin : MonoBehaviour
{
    List<Transform> checkpoint;
    Transform finalCheckpoint;
    List<Transform> exitCheckpoint;
    float speed = 3;
    public bool Exit = false;
    public bool MaskAccepted = false;

    public float WaitMaskTimer = 0;
    float timer = 0;

    private int currentWaypointIndex = 0;

    private void Awake()
    {
        checkpoint = new List<Transform>();
    }

    public void AddExitCheckpoint(Transform transform)
    {
        exitCheckpoint.Add(transform);
    }

    public void AddEnteringCheckpoint(Transform transform)
    {
        checkpoint.Add(transform);
    }

    public void SetFinalCheckpoint(Transform transform)
    {
        finalCheckpoint = transform;
    }

    private void Update()
    {
        if (!Exit)
        {
            if (Vector2.Distance(transform.position, finalCheckpoint.position) > 0.2f)
            {
                if (currentWaypointIndex < (checkpoint.Count))
                {
                    Transform target = checkpoint[currentWaypointIndex];
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                    if (Vector2.Distance(transform.position, target.position) < 0.1f)
                    {

                        currentWaypointIndex++;
                    }
                }
                else
                {
                    Transform target = finalCheckpoint;
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
            }
        }
        else
        {
            //uscita
        }
        if (!MaskAccepted)
        {
            timer += Time.deltaTime;
            if (timer > WaitMaskTimer)
            {
                Exit = true;
                //me ne vado
            }
        }
    }
}


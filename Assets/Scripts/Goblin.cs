using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Goblin : MonoBehaviour
{
    List<Transform> checkpoint;
    Transform finalCheckpoint;
    List<Transform> exitCheckpoints;
    Bed bed;
    float speed = 3;
    public bool Exit = false;
    public bool MaskAccepted = false;
    private bool onBed = false;
    private bool isHappy = true;

    public float WaitMaskTimer = 0;
    float timer = 0;

    private int currentWaypointIndex = 0;
    private int currentWaypointExitIndex = 0;

    private void Awake()
    {
        checkpoint = new List<Transform>();
        exitCheckpoints = new List<Transform>();
    }

    public void AddExitCheckpoint(Transform transform)
    {
        exitCheckpoints.Add(transform);
    }

    public void SetBed(Bed bedValue)
    {
        bed = bedValue;
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
                    if (Vector2.Distance(transform.position, target.position) < 0.2f)
                    {
                        onBed = true;
                    }
                }
            }
        }
        else
        {
            if (currentWaypointExitIndex < (exitCheckpoints.Count))
            {
                Transform target = exitCheckpoints[currentWaypointExitIndex];
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, target.position) < 0.1f)
                {
                    if (currentWaypointExitIndex == 1)
                    {
                        if (isHappy)
                        {
                            Money.Instance.AddMoney(10);
                            GameManager.Instance.AddHappyGoblin();
                        }
                        else
                        {
                            GameManager.Instance.AddAngryGoblin();
                        }
                    }
                    currentWaypointExitIndex++;
                }
            }
            else
            {
                bed.SetBedFreeState();
                Destroy(gameObject);
            }
        }
        if (onBed)
        {
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
}


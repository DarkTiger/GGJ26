using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Goblin : Interactable
{
    [SerializeField] SpriteRenderer spriteRecipe;
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] GameObject maskGameobject;
    List<Transform> checkpoint;
    Transform finalCheckpoint;
    List<Transform> exitCheckpoints;
    Bed bed;
    float speed = 3;
    public bool Exit = false;
    public bool MaskAccepted = false;
    private bool onBed = false;
    private bool isHappy = false;
    private bool move = true;
    private Animator animator;
    private Recipe recipeMask;

    public float WaitMaskTimer = 0;
    float timer = 0;

    private int currentWaypointIndex = 0;
    private int currentWaypointExitIndex = 0;

    private void Awake()
    {
        checkpoint = new List<Transform>();
        exitCheckpoints = new List<Transform>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        ChoseMask();
    }

    public override void OnInteract(Player player)
    {
        base.OnInteract(player);
        if (player.CandidateInteractable == this && player.CurrentItem && player.CurrentItem is Mask)
        {
            if ((player.CurrentItem as Mask).recipe == recipeMask)
            {
                maskGameobject.GetComponent<SpriteRenderer>().color = recipeMask.color;
                maskGameobject.SetActive(true);
                isHappy = true;
                Destroy(player.CurrentItem.gameObject);
                spriteRecipe.enabled = false;
                move = true;
                Exit = true;
            }
        }
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
        if (move)
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
                            animator.SetBool("Move", false);
                            onBed = true;
                            transform.position = finalCheckpoint.parent.position;
                            boxCollider2D.enabled = true;
                            move = false;
                        }
                    }
                }
            }
            else
            {
                boxCollider2D.enabled = false;
                animator.SetInteger("Direction", 2);
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
        }
        if (onBed)
        {
            if (!MaskAccepted)
            {
                timer += Time.deltaTime;
                if (timer > WaitMaskTimer)
                {
                    Exit = true;
                    move = true;
                    //me ne vado
                }
            }
        }
    }
    private void ChoseMask()
    {
        List<Recipe> list = new List<Recipe>();
        list = GameManager.Instance.GetAvaiableRecipe();
        if (list.Count > 0)
        {
            recipeMask = list[Random.Range(0, list.Count)];
            spriteRecipe.sprite = recipeMask.sprite;
        }
    }
}


using System.Collections.Generic;
using UnityEngine;

public class Goblin : Interactable
{
    [SerializeField] SpriteRenderer spriteRecipe;
    [SerializeField] SpriteRenderer comic;
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] GameObject maskGameobject;
    [SerializeField] GameObject angryGameObject;
    [SerializeField] AudioClip angry;
    [SerializeField] AudioClip happy;
    [SerializeField] AudioClip coin;
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
    public Animator maskAnimator, angryAnimator;
    private Recipe recipeMask;
    private int cost = 10;

    public float WaitMaskTimer = 0;
    public float WaitMaskFirstTimer;
    float timer = 0;
    bool firstWait = true;

    private int currentWaypointIndex = 0;
    private int currentWaypointExitIndex = 0;

    [HideInInspector] private Vector2 velocity;
    private Vector2 startPos;

    private void Awake()
    {
        checkpoint = new List<Transform>();
        exitCheckpoints = new List<Transform>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {

        ChoseMask();
        if (GameManager.Instance.GetSpawnCount() == 1)
        {
            firstWait = true;
        }
        else
        {
            firstWait = false;
        }
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
                cost = recipeMask.value;
                isHappy = true;
                MaskAccepted = true;
                AudioSource.PlayClipAtPoint(happy, Camera.main.transform.position, 0.1f);
                Destroy(player.CurrentItem.gameObject);
                spriteRecipe.enabled = false;
                comic.enabled = false;
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
        startPos = transform.position;
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
                            //animator.SetBool("Move", false);
                            SetAnimators(new Vector2(0, -1), 0);
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
                //animator.SetInteger("Direction", 2);
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
                                Money.Instance.AddMoney(cost);
                                GameManager.Instance.AddHappyGoblin();
                                AudioSource.PlayClipAtPoint(coin, Camera.main.transform.position, 0.1f);
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
                if (firstWait)
                {
                    if (timer > WaitMaskFirstTimer)
                    {
                        AudioSource.PlayClipAtPoint(angry, Camera.main.transform.position, 0.1f);
                        Exit = true;
                        move = true;
                        onBed = false;
                        timer = 0;
                        angryGameObject.SetActive(true);
                        print("angry");
                        //me ne vado
                    }
                }
                else
                {
                    if (timer > WaitMaskTimer)
                    {
                        AudioSource.PlayClipAtPoint(angry, Camera.main.transform.position, 0.1f);
                        Exit = true;
                        move = true;
                        onBed = false;
                        timer = 0;
                        angryGameObject.SetActive(true);
                        print("angry");

                        //me ne vado
                    }
                }
            }
        }
    }

    void LateUpdate()
    {
        if (onBed)
            if (MaskAccepted)
            {
                SetAnimators(new Vector2(0, -1), 1);
                return;
            }
            else
            {
                return;
            }

        velocity = new Vector2(transform.position.x, transform.position.y) - startPos;
        if (velocity.magnitude > 0)
            velocity.Normalize();

        SetAnimators(velocity, 1);
    }


    void SetAnimators(Vector2 _velocity, float _speed)
    {
        _velocity.x = Mathf.Round(_velocity.x);
        _velocity.y = Mathf.Round(_velocity.y);

        animator.SetFloat("Horizontal", _velocity.x);
        animator.SetFloat("Vertical", _velocity.y);
        animator.SetFloat("Speed", _speed);
/* 
        maskAnimator.SetFloat("Horizontal", _velocity.x);
        maskAnimator.SetFloat("Vertical", _velocity.y);
        maskAnimator.SetFloat("Speed", _speed);

        angryAnimator.SetFloat("Horizontal", _velocity.x);
        angryAnimator.SetFloat("Vertical", _velocity.y);
        angryAnimator.SetFloat("Speed", _speed); */
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


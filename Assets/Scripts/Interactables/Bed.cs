using UnityEngine;
using UnityEngine.UI;

public class Bed : Interactable
{
    [SerializeField] Sprite freeSprite, busy;
    [SerializeField] Transform checkpointBed;
    [SerializeField] GameObject comicCost;
    public int CostUnlock;
    public bool IsFree = true;
    public bool IsOpen = false;
    private bool comicShow = false;

    public override void OnInteract(Player player)
    {
        base.OnInteract(player);
        if (!IsOpen) 
        {
            if (CostUnlock<= GameManager.Instance.GetMoney())
            {
                GameManager.Instance.DecreaseMoney(CostUnlock);
                GetComponent<SpriteRenderer>().sprite = freeSprite;
                comicCost.SetActive(false);
                IsFree = true;
                IsOpen = true;
            }
        }
    }

    public override void OnEntering(Player player)
    {
        base.OnEntering(player);

        if (!IsOpen)
        {
            comicShow = true;
            comicCost.SetActive(true);
        }
    }

    public override void OnExit(Player player)
    {
        base.OnExit(player);
        comicShow = false;
        if (comicCost)
        {
            comicCost.SetActive(false);
        }
    }


    public void SetBedBusyState()
    {
        IsFree = false;
    }
    public void SetBedFreeState()
    {
        IsFree = true;
    }

    public Transform GetCheckpointBed()
    { 
        return checkpointBed;
    }
}

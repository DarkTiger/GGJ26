using UnityEngine;
using UnityEngine.UI;

public class Bed : Interactable
{
    [SerializeField] Sprite freeSprite, busy;
    [SerializeField] Transform checkpointBed;
    public int CostUnlock;
    public bool IsFree = true;
    public bool IsOpen = false;

    public override void OnInteract(Player player)
    {
        base.OnInteract(player);
        if (!IsOpen) //TODO DA AGGIUNGERE IL CONTROLLO SE SI HANNO SOLDI
        {
            if (CostUnlock<= GameManager.Instance.GetMoney())
            {
                GameManager.Instance.DecreaseMoney(CostUnlock);
                GetComponent<SpriteRenderer>().sprite = freeSprite;
                IsFree = true;
                IsOpen = true;
            }
        }
    }

    public void SetBedBusyState()
    {
        IsFree = false;
    }
    public void SetBedFreeState()
    {

    }

    public Transform GetCheckpointBed()
    { 
        return checkpointBed;
    }
}

using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Money : Interactable
{
    public static Money Instance;
    [SerializeField] GameObject spriteGameObject;
    [SerializeField] AudioClip cash;

    private int amount = 0;

    private void Awake()
    {
        if (Instance != null) { Destroy(Instance); } else { Instance = this; }
    }

    public override void OnInteract(Player player)
    {
        base.OnInteract(player);
        GameManager.Instance.IncreaseMoney(amount);
        spriteGameObject.SetActive(false);
        if (amount > 0)
        {
            AudioSource.PlayClipAtPoint(cash, Camera.main.transform.position, 0.1f);
        }
        amount = 0;
    }

    public void AddMoney(int value)
    {
        amount += value;
        if (amount > 0)
        {
            spriteGameObject.SetActive(true);
        }
    }
}

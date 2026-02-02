using UnityEngine;

public class PieMenu : MonoBehaviour
{
    [SerializeField] ItemMenu item1, item2, item3, item4, item5, item6;
    [SerializeField] float threshold;
    [SerializeField] AudioClip buySeedClip;

    bool interacted = false;

    Player player;

    void Update()
    {
        Vector2 stickR = player.MoveAction.ReadValue<Vector2>();
        float angle = Mathf.Atan2(-stickR.y, -stickR.x) * Mathf.Rad2Deg;
        if (stickR.x > threshold || stickR.y > threshold || stickR.x < -threshold || stickR.y < -threshold)
        {
            //item 1
            if (angle > 60 && angle < 120)
            {
                item1.SelectItem();
                item2.DeselectItem();
                item3.DeselectItem();
                item4.DeselectItem();
                item5.DeselectItem();
                item6.DeselectItem();
                GiveItem(item1);
            }
            //item 2
            if (angle > 0 && angle < 60)
            {
                item1.DeselectItem();
                item2.SelectItem();
                item3.DeselectItem();
                item4.DeselectItem();
                item5.DeselectItem();
                item6.DeselectItem();
                GiveItem(item2);
            }
            //item 3
            if (angle > -60 && angle < 0)
            {
                item1.DeselectItem();
                item2.DeselectItem();
                item3.SelectItem();
                item4.DeselectItem();
                item5.DeselectItem();
                item6.DeselectItem();
                GiveItem(item3);
            }
            //item 4
            if (angle > -120 && angle < -60)
            {
                item1.DeselectItem();
                item2.DeselectItem();
                item3.DeselectItem();
                item4.SelectItem();
                item5.DeselectItem();
                item6.DeselectItem();
                GiveItem(item4);
            }
            //item 5
            if (angle > -180 && angle < -120)
            {
                item1.DeselectItem();
                item2.DeselectItem();
                item3.DeselectItem();
                item4.DeselectItem();
                item5.SelectItem();
                item6.DeselectItem();
                GiveItem(item5);
            }
            //item 6
            if (angle > 120 && angle < 180)
            {
                item1.DeselectItem();
                item2.DeselectItem();
                item3.DeselectItem();
                item4.DeselectItem();
                item5.DeselectItem();
                item6.SelectItem();
                GiveItem(item6);
            }
        }
        else
        {
            item1.DeselectItem();
            item2.DeselectItem();
            item3.DeselectItem();
            item4.DeselectItem();
            item5.DeselectItem();
            item6.DeselectItem();
        }
    }

    public void SetPlayer(Player interactPlayer)
    {
        player = interactPlayer;
    }

    public void RemovePlayer()
    {
        player = null;
    }

    public void GiveItem(ItemMenu itemMenu)
    {
        if (player.InteractAction.WasPressedThisFrame() && itemMenu.SeedData.Cost <= GameManager.Instance.GetMoney())
        {
            AudioSource.PlayClipAtPoint(buySeedClip, Camera.main.transform.position, 0.5f);
            Seed seed = Instantiate(itemMenu.SeedData.SeedPrefab).GetComponent<Seed>();
            GameManager.Instance.DecreaseMoney(itemMenu.SeedData.Cost);
            seed.SetSeedData(itemMenu.SeedData);
            player.CurrentInteractable = seed;
            player.CurrentItem = seed;
            seed.OnInteract(player);
            seed.transform.localPosition = new Vector3(-0.5f, 0f, 0f);
            interacted = false;
            gameObject.SetActive(false);
            player.enableControls = true;
        }
    }
}

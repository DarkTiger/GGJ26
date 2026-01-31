using UnityEngine;

public class Item : Interactable
{
    public Player GrabbedBy { get; private set; }


    public override void OnInteract(Player player)
    {
        base.OnInteract(player);

        if (!GrabbedBy)
        {
            Grab(player);
        }
    }
    public override void OnDeInteract(Player player)
    {
        if (GrabbedBy)
        {
            Release();
        }
    }

    void Grab(Player player)
    {
        GrabbedBy = player;
        transform.parent = player.transform;
    }

    void Release(Vector3 releasePos = default)
    {
        GrabbedBy = null;
        transform.parent = null;

        if (releasePos != Vector3.zero)
        {
            transform.position = releasePos;
        }
    }
}

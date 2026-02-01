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

    public virtual void Use(Player player)
    {

    }

    public void Grab(Player player)
    {
        GrabbedBy = player;
        transform.parent = player.transform;
        GetComponent<Collider2D>().enabled = false;
    }

    public void Release(Vector3 releasePos = default)
    {
        if (GrabbedBy)
        {
            GrabbedBy.CurrentItem = null;
            GrabbedBy = null;
        }
        
        transform.parent = null;
        GetComponent<Collider2D>().enabled = true;

        if (releasePos != Vector3.zero)
        {
            transform.position = releasePos;
        }
    }
}
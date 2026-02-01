using UnityEngine;

public class Item : Interactable
{
    [SerializeField] AudioClip useClip;

    public AudioClip UseClip => useClip;
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
        if (useClip)
        {
            AudioSource.PlayClipAtPoint(useClip, Camera.main.transform.position, 0.5f);
        }
    }

    public void Grab(Player player)
    {
        GrabbedBy = player;
        transform.parent = player.transform;
        GetComponent<Collider2D>().enabled = false;
        transform.localPosition = new Vector3((player.LastVerticalValue <= -0.1f ? 0.5f : -0.1f) + player.LastHorizontalValue * 1.25f, player.LastVerticalValue + 0.5f, 0f);
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
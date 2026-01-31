using UnityEngine;

public class Item : Interactable
{
    public Player GrabbedBy { get; private set; }


    public void Grab(Player player)
    {
        GrabbedBy = player;
        transform.parent = player.transform;
    }

    public void Release(Vector3 releasePos = default)
    {
        GrabbedBy = null;
        transform.parent = null;

        if (releasePos != Vector3.zero)
        {
            transform.position = releasePos;
        }
    }
}

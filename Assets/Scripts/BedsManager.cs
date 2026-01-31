using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BedsManager : MonoBehaviour
{
    public static BedsManager Instance;
    [SerializeField] List<Bed> beds;

    private void Awake()
    {
        if (Instance != null) { Destroy(Instance); } else { Instance = this; }
    }

    private void Start()
    {

    }

    public Bed FindFreeBed()
    {
        foreach (Bed item in beds)
        {
            if (item.IsFree && item.IsOpen)
            {
                //letto libero
                return item;
            }
        }
        return null;
    }
}

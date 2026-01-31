using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<Transform> enteringCheckpoint;
    [SerializeField] List<Transform> exitCheckpoint;
    [SerializeField] Transform spwan;
    [SerializeField] GameObject goblinPrefab;
    public Transform finalCheckpoint;

    [SerializeField] float timerSpawn;

    private float timer;


    [ContextMenu("Spawn")]
    public void Spawn()
    {
        Bed bed = BedsManager.Instance.FindFreeBed();
        if (bed != null)
        {
            bed.SetBedBusyState();
            GameObject goblin = Instantiate(goblinPrefab, spwan.position, spwan.rotation);
            Goblin goblinScript = goblin.GetComponent<Goblin>();
            goblinScript.SetFinalCheckpoint(bed.GetCheckpointBed());
            foreach (Transform item in enteringCheckpoint)
            {
                goblinScript.AddEnteringCheckpoint(item);
            }
            foreach (Transform item in exitCheckpoint)
            {
                goblinScript.AddExitCheckpoint(item);
            }
            goblinScript.SetBed(bed);
        }
        else
        {
            Debug.Log("non ci sono letti liberi");
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerSpawn)
        {
            timer = 0;
            Spawn();
        }
    }
}

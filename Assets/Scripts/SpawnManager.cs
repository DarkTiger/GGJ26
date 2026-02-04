using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<Transform> enteringCheckpoint;
    [SerializeField] List<Transform> exitCheckpoint;
    [SerializeField] Transform spwan;
    [SerializeField] List<GameObject> goblinPrefabList;
    public Transform finalCheckpoint;

    [SerializeField] float startDelay = 10;
    [SerializeField] float timerSpawn;

    private float timer;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        Bed bed = BedsManager.Instance.FindFreeBed();
        if (bed != null)
        {
            bed.SetBedBusyState();
            GameObject goblin = Instantiate(goblinPrefabList[Random.Range(0, goblinPrefabList.Count)], spwan.position, spwan.rotation);
            //GameObject goblin = Instantiate(goblinPrefabList[0], spwan.position, spwan.rotation);
            GameManager.Instance.SetSpawnCount();
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
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad < startDelay)
            return;

        timer += Time.deltaTime;
        if (timer > timerSpawn)
        {
            timer = 0;
            Spawn();
        }
    }
}

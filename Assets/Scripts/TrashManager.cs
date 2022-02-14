using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    //Fields
    public GameObject trash;
    private List<GameObject> trashPile;
    private float deviation;
    public float timeStart, timeDelay;
    private bool stopSpawning;

    // Start is called before the first frame update
    void Start()
    {
        trashPile = new List<GameObject>();
        stopSpawning = false;
        InvokeRepeating("SpawnTrash", timeStart, timeDelay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnTrash()
    {
        deviation = Random.Range(-10f, 10f);
        Vector2 spawnLoc = new Vector2(transform.position.x + deviation, transform.position.y);
        trashPile.Add(Instantiate(trash, spawnLoc, transform.rotation));
        if (stopSpawning)
        {
            CancelInvoke("SpawnTrash");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Fields
    private int playerScore;
    private List<GameObject> trashPile;
    public TrashManager trashMgr;
    private float deviation;
    private bool stopTrashSpawning;
    public GameObject trash;
    public Camera cam;
    private float cameraHeight;
    private float cameraWidth;
    public float timeStart, timeDelay;


    public int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //cameraHeight = cam.orthographicSize * 2f;
        //cameraWidth = cameraHeight * cam.aspect;
        //trashPile = new List<GameObject>();
        //InvokeRepeating("SpawnTrash", timeStart, timeDelay);
    }

    // Update is called once per frame
    void Update()
    {
        //DespawnTrash();
        
    }

    //public void SpawnTrash()
    //{
    //    trashMgr.GetComponent<TrashManager>().SpawnTrash();

    //    //Continues spawning until stopSpawning becomes true
    //    if (stopTrashSpawning)
    //    {
    //        CancelInvoke("SpawnTrash");
    //    }
    //}

    //public void DespawnTrash()
    //{
    //    for (int i = 0; i < trashPile.Count; i++)
    //    {
    //        if (trashPile[i].transform.position.y < cam.transform.position.y - (cameraHeight / 2) - 1)
    //        {
    //            trashMgr.GetComponent<TrashManager>().DespawnTrash(trashPile[i]);
    //            trashPile.Remove(trashPile[i]);
    //        }
    //    }
    //}

    //public void AddTrash(GameObject trashPiece)
    //{
    //    trashPile.Add(trashPiece);
    //}

    public void IncrementScore()
    {
        playerScore += 100;
    }
}

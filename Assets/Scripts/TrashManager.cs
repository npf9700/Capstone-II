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
    public Camera cam;
    private float cameraHeight;
    private float cameraWidth;
    public GameManager gameMgr;

    public List<GameObject> TrashPile
    {
        get { return trashPile; }
    }

    // Start is called before the first frame update
    void Start()
    {
        trashPile = new List<GameObject>();
        stopSpawning = false;
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        InvokeRepeating("SpawnTrash", timeStart, timeDelay);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < trashPile.Count; i++)
        {
            DespawnTrash(trashPile[i]);
        }
    }

    public void SpawnTrash()
    {
        //Gets a random value to spawn the trash above the camera window
        deviation = Random.Range(-10f, 10f);
        Vector2 spawnLoc = new Vector2(transform.position.x + deviation, transform.position.y);
        trashPile.Add(Instantiate(trash, spawnLoc, transform.rotation));
        //gameMgr.GetComponent<GameManager>().AddTrash(newTrash);
    }

    public void DespawnTrash(GameObject trashPiece)
    {
        //Despawns trash at the bottom of the screen
        if (trashPiece.transform.position.y < cam.transform.position.y - (cameraHeight / 2) - 1)
        {
            gameMgr.GetComponent<GameManager>().DecrementScore(200);
            gameMgr.GetComponent<GameManager>().TrashNotCaught();
            trashPile.Remove(trashPiece);
            Destroy(trashPiece);//Trash is removed from the list and hierarchy
        }

        //Despawns trash at the side of the screen, which increments the player's score
        if(trashPiece.transform.position.x < cam.transform.position.x - (cameraWidth / 2) - 0.5 || trashPiece.transform.position.x > cam.transform.position.x + (cameraWidth / 2) + 0.5)
        {
            Debug.Log("Trash was removed by the player");
            gameMgr.GetComponent<GameManager>().IncrementScore(50);
            trashPile.Remove(trashPiece);
            Destroy(trashPiece);//Trash is removed from the list and hierarchy
        }
    }
}

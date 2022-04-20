using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    //Fields
    public GameObject sixPack;
    public GameObject can;
    public GameObject bottle;
    public GameObject barrel;
    public GameObject bag;
    public GameObject straws;

    private List<GameObject> trashPile;
    private List<GameObject> trashObjects;

    private float deviation;
    public float timeStart, timeDelay;
    private bool stopSpawning;
    public Camera cam;
    private float cameraHeight;
    private float cameraWidth;
    public GameManager gameMgr;

    public GameObject starGauge;

    public List<GameObject> TrashPile
    {
        get { return trashPile; }
    }

    // Start is called before the first frame update
    void Start()
    {
        trashPile = new List<GameObject>();
        trashObjects = new List<GameObject>();
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
        TrashBehindOil();
    }

    public void SpawnTrash()
    {
        if(trashObjects.Count == 0)
        {
            AddTrashObjects();
        }

        int trashOption = Random.Range(0, trashObjects.Count);

        //Gets a random value to spawn the trash above the camera window
        deviation = Random.Range(-10f, 10f);
        Vector2 spawnLoc = new Vector2(transform.position.x + deviation, transform.position.y);
        trashPile.Add(Instantiate(trashObjects[trashOption], spawnLoc, transform.rotation));
        //gameMgr.GetComponent<GameManager>().AddTrash(newTrash);
    }

    public void DespawnTrash(GameObject trashPiece)
    {
        //Despawns trash at the bottom of the screen
        if (trashPiece.transform.position.y < cam.transform.position.y - (cameraHeight / 2) - 1)
        {
            gameMgr.GetComponent<GameManager>().TrashNotCaught();
            trashPile.Remove(trashPiece);
            Destroy(trashPiece);//Trash is removed from the list and hierarchy
        }

        //Despawns trash at the side of the screen, which increments the player's score
        if(trashPiece.transform.position.x < cam.transform.position.x - (cameraWidth / 2) - 0.5 || trashPiece.transform.position.x > cam.transform.position.x + (cameraWidth / 2) + 0.5)
        {
            gameMgr.GetComponent<GameManager>().ammoSlider.value += 2;
            gameMgr.trashRemoved += 1;
            float sliderVal = gameMgr.GetComponent<GameManager>().ammoSlider.value;
            gameMgr.GetComponent<GameManager>().ammoText.text = gameMgr.GetComponent<GameManager>().ammoSlider.value.ToString();
            
            trashPile.Remove(trashPiece);
            Destroy(trashPiece);//Trash is removed from the list and hierarchy
            AkSoundEngine.PostEvent("RemoveTrash", gameObject);
            Instantiate(starGauge, new Vector3(8.5f, -4.8f + (sliderVal * 0.8f), 0f), Quaternion.identity);
        }
    }
    //-.4, -1.4, -2.4, -3.4, -4.4
    //Checking if the trash in the trashPile is behind the oil and changing their respective bool values to show that
    public void TrashBehindOil()
    {
        for(int i = 0; i < trashPile.Count; i++)
        {
            if (gameMgr.GetComponent<GameManager>().IsBehindOilSlick())
            {
                trashPile[i].GetComponent<Trash>().IsBehindOil = true;
            }
            else
            {
                trashPile[i].GetComponent<Trash>().IsBehindOil = false;
            }
        }
    }

    public void AddTrashObjects()
    {
        trashObjects.Add(can);
        trashObjects.Add(sixPack);
        trashObjects.Add(bag);
        trashObjects.Add(straws);
        trashObjects.Add(barrel);
        trashObjects.Add(bottle);
    }
}

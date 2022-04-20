using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 spawnPos;
    public GameObject bubblePrefab;
    public List<GameObject> bubbles;
    Camera cam;
    float cameraWidth;
    float cameraHeight;
    GameObject local;
    Movement movement;
    public GameObject animalMgr;
    public GameManager gameMgr;
    public float spawnDistance = 10f;
    float bubbleRadius;
    public float spawnTime;
    Collider2D[] colliders;
    public bool spawned = false;
    private int playerAmmo;

    public GameObject pop;
    private List<GameObject> pops;

    public GameObject bubbleTrail;
    private List<GameObject> trails;

    void Start()
    {
        movement = GameObject.FindObjectOfType(typeof(Movement)) as Movement;
        cam = Camera.main;
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        bubbles = new List<GameObject>();
        pops = new List<GameObject>();
        trails = new List<GameObject>();
        gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnTime = Random.Range(1.5f, 2.5f);
        Debug.Log("Spawn time: " + spawnTime);
        InvokeRepeating("SpawnBubbles", 0.0f, spawnTime);
        InvokeRepeating("IncreaseSpawnTime", 20.0f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        BubbleBehindOil();
      //  maxSpawnTime += 0.03f;

    }
    public void IncreaseSpawnTime()
    {
        float increment = 0.0003f;
        float newSpawnTime = spawnTime + increment;
       // Debug.Log("New Spawn Time: " + newSpawnTime);
        InvokeRepeating("SpawnBubbles", 0.0f, newSpawnTime);
    }

    public void SpawnBubbles()
    {
        if(bubbles.Count >= 5) //cap number on screen to 5
        {
            return;
        } 
        else
        {
            spawnPos = new Vector2(Random.Range(50, (cameraWidth - 120)), cameraHeight);
            local = Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
            bubbles.Add(local);
            movement.FloatUp();
            spawned = true;
        }
        
    }

    public void Despawn(GameObject bubble)
    {
        Destroy(bubble);
        Debug.Log(bubbles.Count);
        bubbles.Remove(bubble);
        
    }

    public void SpawnAnimal(GameObject bubble, int animalOption)
    {
        if (gameMgr.ammoSlider.value == 0) //can't pop bubbles if ammo = 0
        {
            return;
        }
        else
        {
            //Spawns the animal sprite before the location is lost
            animalMgr.GetComponent<AnimalManager>().FreeAnimal(bubble.transform.position, animalOption);
            //Stores position of bubble
            Vector3 bubbleSpot = bubble.transform.position;

            //Gets rid of bubble
            Destroy(bubble);
            bubbles.Remove(bubble);

            float popScale;
            float trailScale;
            float popSize = 0.25f;
            float trailSize = 0.15f;

            //Plays the pop animation
            GameObject bubblePop = Instantiate(pop, bubbleSpot, Quaternion.identity);
            GameObject trailBubbles = Instantiate(bubbleTrail, bubbleSpot, Quaternion.identity);
            if (animalOption == 2 || animalOption == 3)
            {
                popScale = 1f * popSize;
                trailScale = 1f * trailSize;
            }
            else if (animalOption == 0 || animalOption == 1 || animalOption == 4)
            {
                popScale = 2f * popSize;
                trailScale = 2f * trailSize;
            }
            else
            {
                popScale = 3f * popSize;
                trailScale = 3f * trailSize;
            }
            Vector3 newPopScale = new Vector3(popScale, popScale, 1f);
            Vector3 newTrailScale = new Vector3(trailScale, trailScale, 1f);
            bubblePop.transform.localScale = newPopScale;
            trailBubbles.transform.localScale = newTrailScale;
            pops.Add(bubblePop);
            trails.Add(trailBubbles);

            //Increments score
            gameMgr.IncrementScore(100);
            gameMgr.numAnimalsSaved += 1;
        }
    }

    //Checks if the bubble collides with the oil slick rect
    public void BubbleBehindOil()
    {
        for (int i = 0; i < bubbles.Count; i++)
        {
            if (gameMgr.GetComponent<GameManager>().IsBehindOilSlick())
            {
                bubbles[i].GetComponent<Movement>().IsBehindOil = true;
            }
            else
            {
                bubbles[i].GetComponent<Movement>().IsBehindOil = false;
            }
        }
    }
}

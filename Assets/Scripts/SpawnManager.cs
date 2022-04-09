using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //public GameObject smallPenguin;
    //public GameObject smallTurtle;
    //public GameObject smallVaquita;
    //public GameObject smallHumphead;
    //public GameObject smallDolphin;
    //public GameObject smallWhaleshark;
    //public GameObject smallSeal;
    
    void Start()
    {
        movement = GameObject.FindObjectOfType(typeof(Movement)) as Movement;
        cam = Camera.main;
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        bubbles = new List<GameObject>();
        gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnTime = Random.Range(1.5f, 2.5f);
        Debug.Log("Spawn time: " + spawnTime);
        InvokeRepeating("SetPingPongSpeed", 0.0f, spawnTime);
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
        Debug.Log("New Spawn Time: " + newSpawnTime);
        InvokeRepeating("SetPingPongSpeed", 0.0f, newSpawnTime);
    }

    public void SetPingPongSpeed()
    {
        float pingPongSpeed = Random.Range(0.4f, 0.8f); //was higher range before, decreased to make it seem less robotic
        SpawnBubbles(pingPongSpeed);
        
    }

    public void SpawnBubbles(float pingPongSpeed)
    {
        if(bubbles.Count >= 5) //cap number on screen to 5
        {
            return;
        } 
        else
        {
            spawnPos = new Vector2(Random.Range(20, (cameraWidth - 90)), cameraHeight);
            local = Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
            //GameObject childSprite;
            //switch (local.GetAnimState())
            //{
            //    case 0:
            //        childSprite = Instantiate(smallTurtle, spawnPos, Quaternion.identity);
            //        break;
            //    case 1:
            //        childSprite = Instantiate(smallSeal, spawnPos, Quaternion.identity);
            //        break;
            //    case 2:
            //        childSprite = Instantiate(smallDolphin, spawnPos, Quaternion.identity);
            //        break;
            //    case 3:
            //        childSprite = Instantiate(smallHumphead, spawnPos, Quaternion.identity);
            //        break;
            //    case 4:
            //        childSprite = Instantiate(smallPenguin, spawnPos, Quaternion.identity);
            //        break;
            //    case 5:
            //        childSprite = Instantiate(smallWhaleshark, spawnPos, Quaternion.identity);
            //        break;
            //    case 6:
            //        childSprite = Instantiate(smallVaquita, spawnPos, Quaternion.identity);
            //        break;
            //    default:
            //        childSprite = Instantiate(smallTurtle, spawnPos, Quaternion.identity);
            //        break;
            //}
            //Debug.Log(local.GetAnimState());
            //childSprite.transform.parent = local.transform;
            bubbles.Add(local);
            movement.FloatUp(pingPongSpeed);
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
            //Gets rid of bubble
            Destroy(bubble);
            bubbles.Remove(bubble);

            //Increments score
            gameMgr.IncrementScore(100);
            gameMgr.ammoSlider.value -= 1;
            Debug.Log("Ammo: " + gameMgr.ammoSlider.value);
            gameMgr.ammoText.text = gameMgr.ammoSlider.value.ToString();
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

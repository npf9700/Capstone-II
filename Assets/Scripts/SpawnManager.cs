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

    void Start()
    {
        movement = GameObject.FindObjectOfType(typeof(Movement)) as Movement;
        cam = Camera.main;
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        bubbles = new List<GameObject>();
        
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
        Debug.Log("Spawn Time Increased from " + spawnTime + "to " + newSpawnTime);
        InvokeRepeating("SetPingPongSpeed", 0.0f, newSpawnTime);
    }

    public void SetPingPongSpeed()
    {
        float pingPongSpeed = Random.Range(1f, 1.1f);
        SpawnBubbles(pingPongSpeed);
        
    }

    public void SpawnBubbles(float pingPongSpeed)
    {
        if (bubbles.Count > 7)
        {
            return;
        }
        else
        {
            spawnPos = new Vector2(Random.Range(20, (cameraWidth - 90)), cameraHeight);
            local = Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
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

    public void SpawnAnimal(GameObject bubble)
    {
        //Spawns the animal sprite before the location is lost
        animalMgr.GetComponent<AnimalManager>().FreeAnimal(bubble.transform.position);

        //Gets rid of bubble
        Destroy(bubble);
        bubbles.Remove(bubble);

        //Increments score
        gameMgr.GetComponent<GameManager>().IncrementScore(100);

    }


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

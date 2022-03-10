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
        spawnTime = Random.Range(2f, 3f);
        
        //this is to avoid overlap but it's not working, idk why.
        //while(!canSpawn)
        //{
        //    canSpawn = PreventOverlap();
        //    if(canSpawn)
        //    {
        //        break;
        //    }
        //}
        InvokeRepeating("SetPingPongSpeed", 0.0f, spawnTime);




    }

    // Update is called once per frame
    void Update()
    {
        BubbleBehindOil();
    }

    public void SetPingPongSpeed()
    {
        float pingPongSpeed = Random.Range(1.05f, 1.1f);
        SpawnBubbles(pingPongSpeed);
    }

    public void SpawnBubbles(float pingPongSpeed)
    {
        spawnPos = new Vector2(Random.Range(20, (cameraWidth - 90)), cameraHeight);
        local = Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
        bubbles.Add(local);
        movement.FloatUp(pingPongSpeed);
        spawned = true;
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

    //public void Respawn()
    //{
    //    SpawnBubbles();
    //}


    /// <summary>
    /// This entire method does not work yet. 
    /// I tried looking at tutorials and found this video to help, 
    /// but I don't get why it's not working.
    /// https://www.youtube.com/watch?v=t2Cs71rDlUg
    /// </summary>
    /// <returns></returns>
    bool PreventOverlap()
    {
        bubbleRadius = GameObject.Find("Bubble").GetComponent<CircleCollider2D>().radius;
        Debug.Log(bubbleRadius);
        colliders = Physics2D.OverlapCircleAll(spawnPos, bubbleRadius);
        for(int i = 0; i < colliders.Length; i++)
        {
            Vector3 center = colliders[i].bounds.center;
            float xPosLeft = center.x - colliders[i].bounds.extents.x;
            float xPosRight = center.x + colliders[i].bounds.extents.x;
            float lowerYPos = center.y - colliders[i].bounds.extents.y;
            float upperYPos = center.y + colliders[i].bounds.extents.y;
            if (spawnPos.x >= xPosLeft && spawnPos.x <= xPosRight)
            {
                if (spawnPos.y >= lowerYPos && spawnPos.y <= upperYPos)
                {
                    return (false);
                }
            }
        }
        return (true);
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

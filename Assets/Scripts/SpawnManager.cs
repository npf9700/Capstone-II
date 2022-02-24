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
    public float spawnDistance = 10f;
    float bubbleRadius;
    Collider2D[] colliders;

    public bool spawned = false;

    void Start()
    {
        movement = GameObject.FindObjectOfType(typeof(Movement)) as Movement;
        cam = Camera.main;
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        bubbles = new List<GameObject>();

        //this is to avoid overlap but it's not working, idk why.
        //while(!canSpawn)
        //{
        //    canSpawn = PreventOverlap();
        //    if(canSpawn)
        //    {
        //        break;
        //    }
        //}
        InvokeRepeating("SpawnBubbles", 0.0f, 3.0f);




    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBubbles()
    {
        spawnPos = new Vector2(Random.Range(20, (cameraWidth - 90)), cameraHeight);
        local = Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
        bubbles.Add(local);
        movement.FloatUp();
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

        Destroy(bubble);
        bubbles.Remove(bubble);
        
    }

    public void Respawn()
    {
        SpawnBubbles();
    }


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
}

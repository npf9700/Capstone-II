using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 spawnPos;
    public GameObject bubblePrefab;
    public List<GameObject> bubbles;
    Camera cam;
    float cameraWidth;
    float cameraHeight;
    Movement movement;

    public bool spawned = false;

    void Start()
    {
        movement = GameObject.FindObjectOfType(typeof(Movement)) as Movement;
        cam = Camera.main;
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
       
        bubbles = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            SpawnBubbles();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnBubbles()
    {
        spawnPos = new Vector3(Random.Range(0, cameraWidth), cameraHeight, 0);
        GameObject b = Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
        bubbles.Add(b);
        movement.FloatUp();
        spawned = true;
    }

    //public void Despawn(GameObject bubble)
    //{
    //    GameObject b = Instantiate(bubble, bubble.transform.position, Quaternion.identity);
    //    bubbles.Remove(b);
    //    Destroy(b);
    //    if (bubbles.Count == 0)
    //    {
    //        for (int i = 0; i < 5; i++)
    //        {
    //            SpawnBubbles();
    //        }
    //    }
    //}
}

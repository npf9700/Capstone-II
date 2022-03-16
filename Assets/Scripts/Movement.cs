using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public Vector3 speed;
    Camera cam;
    float cameraWidth;
    float cameraHeight;
    public Vector2 currentPos;
    public SpawnManager spm;
    float pingPongSpeed;
    private bool isBehindOil;

    public bool IsBehindOil
    {
        get { return isBehindOil; }
        set { isBehindOil = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        isBehindOil = false;
        spm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
       // spm = GameObject.FindObjectOfType(typeof(SpawnManager)) as SpawnManager;
        cam = Camera.main;
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        speed = new Vector3(0f, Random.Range(0f, cameraHeight)) * Time.deltaTime;
        speed = Vector3.ClampMagnitude(speed, 0.005f);
        currentPos = new Vector2(Random.Range(cam.transform.position.x - cameraWidth / 2, cam.transform.position.x + cameraWidth / 2), cam.transform.position.y - cameraHeight / 2 - 1);
        pingPongSpeed = Random.Range(1.05f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
        //spawned boolean is set to true after initial call to FloatUp() method in SpawnManager.cs
        if (spm.spawned)
        {
            FloatUp(pingPongSpeed);
        }
        DespawnAtTop();
       


    }


   public void FloatUp(float pingPongSpeed)
   {
        float time = Mathf.PingPong(Time.time * pingPongSpeed, 1);
        currentPos.y += speed.y; //change y coordinate based on speed
        Vector2 newPos = new Vector2(currentPos.x + 2, currentPos.y);
        transform.position = Vector2.Lerp(currentPos, newPos, time);
   }

   public void DespawnAtTop()
   {
        if (transform.position.y > cam.transform.position.y + cameraHeight / 2 + 1)
        {
            for (int j = 0; j < spm.bubbles.Count; j++)
            {
                if (spm.bubbles[j] == gameObject)
                {
                    spm.Despawn(spm.bubbles[j]);
                    
                    if(spm.bubbles.Count < 2)
                    {
                        float spawnIncrement = 0.0003f;
                        spm.IncreaseSpawnTime(spawnIncrement);
                    } 
                    else
                    {
                        break;
                    }
                   
                    
                    
                    //if (spm.bubbles.Count == 0)
                    //{


                    //    InvokeRepeating("spm.Respawn", 0.0f, spm.spawnTime);

                    //}
                }
            }
        }
    }

   void OnMouseDown()
   {
        for (int j = 0; j < spm.bubbles.Count; j++)
        {
            if(spm.bubbles[j] == gameObject && isBehindOil == false)
            {
                spm.SpawnAnimal(spm.bubbles[j]);

                if (spm.bubbles.Count == 0)
                {
                    float spawnIncrement = 0.0003f;
                    spm.IncreaseSpawnTime(spawnIncrement);
                }
            }
            
        }
        
    }
}

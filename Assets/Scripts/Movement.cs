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
    float pingPongSpeed = 1.05f;
    // Start is called before the first frame update
    void Start()
    {
        spm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
       // spm = GameObject.FindObjectOfType(typeof(SpawnManager)) as SpawnManager;
        cam = Camera.main;
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        speed = new Vector3(0f, Random.Range(0f, cameraHeight)) * Time.deltaTime;
        speed = Vector3.ClampMagnitude(speed, 0.005f);
        currentPos = new Vector2(Random.Range(cam.transform.position.x - cameraWidth / 2, cam.transform.position.x + cameraWidth / 2), cam.transform.position.y - cameraHeight / 2 - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
        //spawned boolean is set to true after initial call to FloatUp() method in SpawnManager.cs
        if (spm.spawned)
        {
            FloatUp();
        }
        DespawnAtTop();
       


    }


   public void FloatUp()
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
                    if (spm.bubbles.Count == 0)
                    {
                        
                       InvokeRepeating("spm.SpawnBubbles()", 0.0f, 2.0f);
                       
                    }
                }
            }
        }
    }

   void OnMouseDown()
   {
        for (int j = 0; j < spm.bubbles.Count; j++)
        {
            if(spm.bubbles[j] == gameObject)
            {
                spm.SpawnAnimal(spm.bubbles[j]);
                if (spm.bubbles.Count == 0)
                {
                    InvokeRepeating("spm.SpawnBubbles()", 0.0f, 2.0f);
                }
            }
            
        }
        
    }
}

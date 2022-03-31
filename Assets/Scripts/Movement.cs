using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public Vector3 speed;
    protected Camera cam;
    protected float cameraWidth;
    protected float cameraHeight;
    public Vector2 currentPos;
    public SpawnManager spm;
    protected float pingPongSpeed;
    private bool isBehindOil;
    private float size;
    private int hitsLeft;
    private Vector3 newScale = new Vector3(1f, 1f, 1f);

    public bool IsBehindOil
    {
        get { return isBehindOil; }
        set { isBehindOil = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        size = DetermineSize();
        ScaleBubble();
        hitsLeft = (int)size;
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
        if (this.transform.position.y > cam.transform.position.y + cameraHeight / 2 + 1)
        {
            for (int j = 0; j < spm.bubbles.Count; j++)
            {
                if (spm.bubbles[j] == gameObject)
                {
                    spm.Despawn(spm.bubbles[j]);
                }
            }
        }
    }

   void OnMouseDown()
   {
        hitsLeft--;
        for (int j = 0; j < spm.bubbles.Count; j++)
        {
            if(spm.bubbles[j] == gameObject && isBehindOil == false && hitsLeft <= 0)
            {
                spm.SpawnAnimal(spm.bubbles[j]);
            }
            
        }
        
    }

    //Randomly determines if the bubble should be small, medium, or large
    //(Randomness is weighted towards the smaller bubbles)
    private float DetermineSize()
    {
        float scaleRank = Random.Range(0f, 10f);
        float bubbleSize = 0;
        if(scaleRank <= 5f)
        {
            bubbleSize = 1f;
        }
        else if(scaleRank <= 8.5f)
        {
            bubbleSize = 2f;
        }
        else
        {
            bubbleSize = 3f;
        }
        return bubbleSize;
    }

    //Changes the scale value of the bubble accordingly
    private void ScaleBubble()
    {
        newScale = new Vector3(size, size, 1f);
        transform.localScale = newScale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AnimalState
{
    turtle,
    seal,
    dolphin,
    humphead,
    penguin,
    whaleshark,
    vaquita
}
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
    public GameManager gmr;
    private AnimalState animalState;

    public GameObject smallTurtle;
    public GameObject smallSeal;
    public GameObject smallDolphin;
    public GameObject smallPenguin;
    public GameObject smallHumphead;
    public GameObject smallVaquita;
    public GameObject smallWhaleshark;

    public bool IsBehindOil
    {
        get { return isBehindOil; }
        set { isBehindOil = value; }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        hitsLeft = (int)size;
        isBehindOil = false;
        spm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        cam = Camera.main;
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        speed = new Vector3(0f, Random.Range(0f, cameraHeight)) * Time.deltaTime;
        speed = Vector3.ClampMagnitude(speed, 0.005f);
        currentPos = new Vector2(Random.Range(cam.transform.position.x - cameraWidth / 2, cam.transform.position.x + cameraWidth / 2), cam.transform.position.y - cameraHeight / 2 - 1);
        pingPongSpeed = Random.Range(1.05f, 1.1f);
        gmr = GameObject.Find("GameManager").GetComponent<GameManager>();
        animalState = (AnimalState)Random.Range(0, 7);
        size = DetermineSize();
        ScaleBubble();
        GameObject childSprite;
        switch ((int)animalState)
        {
            case 0:
                childSprite = Instantiate(smallTurtle, transform.position, Quaternion.identity);
                break;
            case 1:
                childSprite = Instantiate(smallDolphin, transform.position, Quaternion.identity);
                break;
            case 2:
                childSprite = Instantiate(smallHumphead, transform.position, Quaternion.identity);
                break;
            case 3:
                childSprite = Instantiate(smallPenguin, transform.position, Quaternion.identity);
                break;
            case 4:
                childSprite = Instantiate(smallSeal, transform.position, Quaternion.identity);
                break;
            case 5:
                childSprite = Instantiate(smallWhaleshark, transform.position, Quaternion.identity);
                break;
            case 6:
                childSprite = Instantiate(smallVaquita, transform.position, Quaternion.identity);
                break;
            default:
                childSprite = Instantiate(smallTurtle, transform.position, Quaternion.identity);
                break;
        }
        childSprite.transform.parent = transform;
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
        gmr.ammoSlider.value -= 1;
        for (int j = 0; j < spm.bubbles.Count; j++)
        {
            if(spm.bubbles[j] == gameObject && isBehindOil == false && hitsLeft <= 0)
            {
                spm.SpawnAnimal(spm.bubbles[j], (int)animalState);
            }
            
        }
        
    }

    //Randomly determines if the bubble should be small, medium, or large
    //(Randomness is weighted towards the smaller bubbles)
    private float DetermineSize()
    {
        float bubbleSize = 0;
        if((int)animalState == 2 || (int)animalState == 3)
        {
            bubbleSize = 1f;
        }
        else if((int)animalState == 0 || (int)animalState == 1 || (int)animalState == 4)
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
        newScale = new Vector3(size * 0.15f, size * 0.15f, 1f);
        transform.localScale = newScale;
    }

  
}

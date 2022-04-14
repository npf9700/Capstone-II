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
    vaquita,
    otter,
    rightWhale,
    blueWhale
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
    private SpriteRenderer bubbleRend;

    public GameObject smallTurtle;
    public GameObject smallSeal;
    public GameObject smallDolphin;
    public GameObject smallPenguin;
    public GameObject smallHumphead;
    public GameObject smallVaquita;
    public GameObject smallWhaleshark;
    public GameObject smallOtter;
    public GameObject smallRightWhale;
    public GameObject smallBlueWhale;

    public bool IsBehindOil
    {
        get { return isBehindOil; }
        set { isBehindOil = value; }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        hitsLeft = 0;
        isBehindOil = false;
        spm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        cam = Camera.main;
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        speed = new Vector3(0f, Random.Range(0f, cameraHeight)) * Time.deltaTime;
        speed = Vector3.ClampMagnitude(speed, 0.005f);
        currentPos = new Vector2(Random.Range(cam.transform.position.x - cameraWidth / 2, cam.transform.position.x + cameraWidth / 2), cam.transform.position.y - cameraHeight / 2 - 1);
        pingPongSpeed = Random.Range(0.4f, 0.8f);
        gmr = GameObject.Find("GameManager").GetComponent<GameManager>();
        bubbleRend = gameObject.GetComponent<SpriteRenderer>();

        animalState = (AnimalState)Random.Range(0, 10);
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
            case 7:
                childSprite = Instantiate(smallOtter, transform.position, Quaternion.identity);
                break;
            case 8:
                childSprite = Instantiate(smallBlueWhale, transform.position, Quaternion.identity);
                break;
            case 9:
                childSprite = Instantiate(smallRightWhale, transform.position, Quaternion.identity);
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
        Vector2 newPos = new Vector2(currentPos.x + 2, currentPos.y + 0.5f);
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
        if (gmr.ammoSlider.value != 0 && isBehindOil == false)
        {
            hitsLeft--;
        }
        gmr.ammoSlider.value -= 1;
        for (int j = 0; j < spm.bubbles.Count; j++)
        {
            if (spm.bubbles[j] == gameObject)
            {
                if (isBehindOil == false && hitsLeft <= 0)
                {
                    AkSoundEngine.PostEvent("PopBubble", gameObject);//Bubble Sound Effect
                    spm.SpawnAnimal(spm.bubbles[j], (int)animalState);
                }
                else if (isBehindOil == true)
                {
                    AkSoundEngine.PostEvent("ClickBubble", gameObject);//Click Sound Effect
                    gmr.ammoSlider.value += 1;
                }
                else
                {
                    AkSoundEngine.PostEvent("PopBubble", gameObject);//Bubble Sound Effect
                }
                if((size == 3 && hitsLeft == 2) || (size == 2 && hitsLeft == 1))
                {
                    bubbleRend.color = new Color(1, 0, 1, 0.5f);
                }
                else if(size == 3 && hitsLeft == 1)
                {
                    bubbleRend.color = new Color(1, 0, 0, 0.5f);
                }
            }
            
        }
        gmr.ammoText.text = gmr.ammoSlider.value.ToString();
        Debug.Log("Ammo: " + gmr.ammoSlider.value);

    }

    //Randomly determines if the bubble should be small, medium, or large
    //(Randomness is weighted towards the smaller bubbles)
    private float DetermineSize()
    {
        float bubbleSize = 0;
        if((int)animalState == 2 || (int)animalState == 3)
        {
            bubbleSize = 1f;
            hitsLeft = 1;
        }
        else if((int)animalState == 0 || (int)animalState == 1 || (int)animalState == 4 || (int)animalState == 7)
        {
            bubbleSize = 2f;
            hitsLeft = 2;
        }
        else
        {
            bubbleSize = 3f;
            hitsLeft = 3;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    //Fields
    private List<GameObject> animals;
    private List<GameObject> creatures;
    private List<GameObject> freed;
    private List<int> freedOptions;
    private List<GameObject> swimming;

    public GameObject turtle;
    public GameObject dolphin;
    public GameObject humphead;
    public GameObject penguin;
    public GameObject seal;
    public GameObject whaleShark;
    public GameObject vaquita;
    public GameObject otter;
    public GameObject blueWhale;
    public GameObject rightWhale;

    //public GameObject smallTurtle;
    //public GameObject smallDolphin;
    //public GameObject smallHumphead;
    //public GameObject smallPenguin;
    //public GameObject smallSeal;
    //public GameObject smallWhaleShark;
    //public GameObject smallVaquita;
    //public GameObject smallOtter;
    //public GameObject smallBlueWhale;
    //public GameObject smallRightWhale;

    public Camera cam;
    private float cameraHeight;
    private float cameraWidth;

    private int randAnimIndex;
    private Vector2 direction;
    private Vector2 velocity;
    private Vector2 position;

    public BackgroundAnimalManager backAnimMgr;

    // Start is called before the first frame update
    void Start()
    {
        creatures = new List<GameObject>();
        animals = new List<GameObject>();
        freed = new List<GameObject>();
        swimming = new List<GameObject>();
        freedOptions = new List<int>();
        AddCreatures();
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        Vector2 direction = Vector2.zero;
        Vector2 velocity = Vector2.zero;
        Vector2 position = Vector2.zero;
        backAnimMgr = GameObject.Find("BackgroundAnimalManager").GetComponent<BackgroundAnimalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(freedOptions.Count % 3 == 0 && swimming.Count < 1)
        //{
        //    FreeAnimalSwim();
        //}
        
        //for (int i = 0; i < swimming.Count; i++)
        //{
        //    direction *= 0.005f;
        //    velocity += direction;
        //    position += velocity;
        //    swimming[i].transform.position = position;
        //}
    }

    public void FreeAnimal(Vector2 bubbleSpot, int animalOption)
    {
        //randAnimIndex = Random.Range(0, freedOptions.Count);
        if (creatures == null)
        {
            creatures = new List<GameObject>();
        }
        if(animals == null)
        {
            animals = new List<GameObject>();
        }

        if (creatures.Count == 0)
        {
            AddCreatures();
        }
        animals.Add(Instantiate(creatures[animalOption], bubbleSpot, Quaternion.identity));
        backAnimMgr = GameObject.Find("BackgroundAnimalManager").GetComponent<BackgroundAnimalManager>();
        if (backAnimMgr == null)
        {
            Debug.Log("Null for some reason");
            
        }
        backAnimMgr.AnimalAppear(animalOption);
        //freedOptions.Add(animalOption);
    }

    //USE ANIMAL OPTION TO MAKE A NEW LIST OF FREED ANIMALS
    //SCALE ALL ANIMALS IN THE LIST BY HALF
    //INSTANTIATE AT THE SIDE OF THE SCREEN

    private void AddCreatures()
    {
        creatures.Add(turtle);
        creatures.Add(dolphin);
        creatures.Add(humphead);
        creatures.Add(penguin);
        creatures.Add(seal);
        creatures.Add(whaleShark);
        creatures.Add(vaquita);
        creatures.Add(otter);
        creatures.Add(blueWhale);
        creatures.Add(rightWhale);
        //freed.Add(smallTurtle);
        //freed.Add(smallDolphin);
        //freed.Add(smallHumphead);
        //freed.Add(smallPenguin);
        //freed.Add(smallSeal);
        //freed.Add(smallWhaleShark);
        //freed.Add(smallVaquita);
        //freed.Add(smallOtter);
        //freed.Add(smallBlueWhale);
        //freed.Add(smallRightWhale);
    }

    public void FreeAnimalSwim()
    {
        Debug.Log("Outside if");
        if (freed.Count != 0)
        {
            float screenSideOption = Random.Range(0f, 1f);
            float swimYPos = Random.Range(0f, 4.5f);
            Vector2 swimPos = Vector2.zero;
            if (screenSideOption < 0.5f)
            {
                Debug.Log("Right");
                swimPos = new Vector2((cameraWidth / 2), swimYPos);
                direction = new Vector2(-1, 0);
            }
            else
            {
                Debug.Log("Left");
                swimPos = new Vector2(-1f * (cameraWidth / 2f), swimYPos);
                direction = new Vector2(1, 0);
            }
            position = swimPos;
            swimming.Add(Instantiate(freed[randAnimIndex], swimPos, Quaternion.identity));
            Debug.Log("Background swimmer - index: " + randAnimIndex + "(" + freed[randAnimIndex] + ")");
        }
    }
}

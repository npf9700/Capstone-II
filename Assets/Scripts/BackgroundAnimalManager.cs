using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimalManager : MonoBehaviour
{
    private List<GameObject> backAnimals;
    private List<GameObject> swimming;
    private List<int> indicies;
    public GameObject backTurtle;
    public GameObject backDolphin;
    public GameObject backOtter;
    public GameObject backHumphead;
    public GameObject backPenguin;
    public GameObject backVaquita;
    public GameObject backWhaleshark;
    public GameObject backRightWhale;
    public GameObject backBlueWhale;
    public GameObject backSeal;

    public Camera cam;
    private float cameraHeight;
    private float cameraWidth;

    // Start is called before the first frame update
    void Start()
    {
        backAnimals = new List<GameObject>();
        swimming = new List<GameObject>();
        indicies = new List<int>();
        AddAnimals();

        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;

        //Making the animals invisible
        for (int i = 0; i < backAnimals.Count; i++)
        {
            backAnimals[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }

        InvokeRepeating("BackgroundSwim", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackgroundSwim()
    {
        if(backAnimals.Count == 0)
        {
            AddAnimals();
            for (int i = 0; i < backAnimals.Count; i++)
            {
                backAnimals[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            }
        }
        int randAnimIndex = Random.Range(0, backAnimals.Count);
        float screenSideOption = Random.Range(0f, 1f);
        float swimYPos = Random.Range(0f, 4.5f);
        Vector2 swimPos = Vector2.zero;
        if (screenSideOption < 0.5f)
        {
            swimPos = new Vector2((cameraWidth / 2) + 1, swimYPos);
            backAnimals[randAnimIndex].GetComponent<SpriteRenderer>().flipX = false;
            //direction = new Vector2(-1, 0);
        }
        else
        {
            swimPos = new Vector2(-1f * (cameraWidth / 2f) -1, swimYPos);
            backAnimals[randAnimIndex].GetComponent<SpriteRenderer>().flipX = true;
            //direction = new Vector2(1, 0);
        }
        //position = swimPos;
        Debug.Log("Instantiating animal " + randAnimIndex);
        swimming.Add(Instantiate(backAnimals[randAnimIndex], swimPos, Quaternion.identity));
    }

    public void AnimalAppear(int index)
    {
        backAnimals[index].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        Debug.Log("Animal " + index + " is now visible");
    }

    private void AddAnimals()
    {
        backAnimals.Add(backTurtle);
        backAnimals.Add(backDolphin);
        backAnimals.Add(backHumphead);
        backAnimals.Add(backPenguin);
        backAnimals.Add(backSeal);
        backAnimals.Add(backWhaleshark);
        backAnimals.Add(backVaquita);
        backAnimals.Add(backOtter);
        backAnimals.Add(backBlueWhale);
        backAnimals.Add(backRightWhale);

    }
}

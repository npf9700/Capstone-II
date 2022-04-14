using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    //Fields
    private List<GameObject> animals;
    private List<GameObject> creatures;

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
    

    // Start is called before the first frame update
    void Start()
    {
        creatures = new List<GameObject>();
        animals = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FreeAnimal(Vector2 bubbleSpot, int animalOption)
    {
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

    }

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
    }
}

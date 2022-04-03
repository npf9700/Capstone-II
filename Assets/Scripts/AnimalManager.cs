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
    
    private int randAnim;

    // Start is called before the first frame update
    void Start()
    {
        animals = new List<GameObject>();
        creatures = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FreeAnimal(Vector2 bubbleSpot)
    {
        if(creatures.Count == 0)
        {
            AddCreatures();
        }
        randAnim = Random.Range(0, creatures.Count);
        
        Debug.Log("Size: " + creatures.Count);
        Debug.Log("Index: " + randAnim);
        animals.Add(Instantiate(creatures[randAnim], bubbleSpot, Quaternion.identity));
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
    }
}

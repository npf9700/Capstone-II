using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    //Fields
    public GameObject animal;
    private List<GameObject> animals;

    // Start is called before the first frame update
    void Start()
    {
        animals = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FreeAnimal(Vector2 bubbleSpot)
    {
        animals.Add(Instantiate(animal, bubbleSpot, Quaternion.identity));
    }
}

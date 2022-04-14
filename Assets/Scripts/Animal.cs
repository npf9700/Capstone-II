using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    //Fields
    private Vector2 position;
    private Vector2 direction;
    private Vector2 velocity;
    private float maxSpeed;
    private float accelRate = 0.0005f;
    private Vector2 acceleration = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        float dirChoice = Random.Range(0, 1f);
        if (dirChoice < 0.5f)
        {
            direction = new Vector2(1, 0);
        }
        else
        {
            direction = new Vector2(-1, 0);
            this.GetComponent<SpriteRenderer>().flipX = true;
    }
    maxSpeed = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = direction * accelRate;
        velocity += acceleration;
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
        position += velocity;
        transform.position = position;
    }
}

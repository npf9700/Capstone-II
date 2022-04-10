using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal; 

public class Trash : MonoBehaviour/*, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler*/
{
    //Fields
    private Vector2 position;
    private Vector2 direction;
    private Vector2 velocity;
    private float maxSpeed;
    private float accelRate = 0.0005f;
    private Vector2 acceleration = Vector2.zero;
    private bool isHeld;
    private bool isBehindOil;

    private float startPosX;
    private float startPosY;

    public Sprite notClicked;
    public Sprite clicked;

    private SpriteRenderer trashRend;

    private Light2D trashLight;

    public Vector2 Position
    {
        get { return position; }
    }

    public bool IsBehindOil
    {
        get { return isBehindOil; }
        set { isBehindOil = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        isHeld = false;
        position = transform.position;
        direction = new Vector2(0, -1);
        maxSpeed = 0.01f;
        isBehindOil = false;
        trashRend = gameObject.GetComponent<SpriteRenderer>();
        trashLight = gameObject.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTrash();

        if (isHeld)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            position = new Vector2(mousePos.x - startPosX, mousePos.y - startPosY);

        }
    }

    public void OnMouseDown()
    {
        //Getting mouse position in world
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);


        if (isBehindOil == false)
        {
            trashRend.sprite = clicked;
            trashLight.intensity = 3f;
            startPosX = mousePos.x - transform.position.x;
            startPosY = mousePos.y - transform.position.y;

            isHeld = true;
        }
    }

    public void OnMouseUp()
    {
        trashRend.sprite = notClicked;
        isHeld = false;
        trashLight.intensity = 1f;
    }

    public void MoveTrash()
    {
        transform.Rotate(0f, 0f, 0.1f, Space.Self);
        acceleration = direction * accelRate;
        velocity += acceleration;
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
        position += velocity;
        transform.position = position;
    }

    public void StopTrash()
    {
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
    }
}


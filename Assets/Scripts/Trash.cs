using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private float startPosX;
    private float startPosY;


    // Start is called before the first frame update
    void Start()
    {
        isHeld = false;
        position = transform.position;
        direction = new Vector2(0, -1);
        maxSpeed = 0.01f;

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
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        startPosX = mousePos.x - transform.position.x;
        startPosY = mousePos.y - transform.position.y;

        isHeld = true;
    }

    public void OnMouseUp()
    {
        isHeld = false;

    }
    //Events

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    Debug.Log("Change cursor icon here");

    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.Log("Change cursor back to original");
    //    MoveTrash();
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("Click");
    //    StopTrash();
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    position = transform.position;
    //    position += eventData.delta;
    //    transform.position = position;
    //}

    public void MoveTrash()
    {
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


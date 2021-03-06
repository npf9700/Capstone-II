using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using WiimoteApi;

public class PointerManager : MonoBehaviour
{
    public Camera cam;
    private float cameraHeight;
    private float cameraWidth;
    private float xOffset;
    private float yOffset;
    private Quaternion initial_rotation;

    private Wiimote wiimote1;
    public GameObject P1_IR;
    public Vector2 P1_IRPosition;
    public GameObject P1_Cursor;
    public Vector2 P1_CursorPosition;
    public float moveSpeed = 20f;
    public bool P1_Popping = false;
    public bool P1_Grabbing = false;

    private Wiimote wiimote2;
    private GameObject P2_IRLocation;
    public GameObject P2_CursorLocation;

    public GameObject myPrefab;
    public bool usingMouse;

    public Sprite cursorActive;
    public Sprite cursorHoldTrash;
    public Sprite cursorHover;
    public Sprite cursorPopBubble;

    public bool popTimerRunning = false;
    public float popTimer;
    public bool isBeingAltered = false;

    public GameObject myButton;
    public BoxCollider2D buttonCollider;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        // Setting up camera values
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        xOffset = cameraWidth / 2;
        yOffset = cameraHeight / 2;
        Debug.Log("camera width and height: " + cameraWidth + ", " + cameraHeight);

        // Bounds Calibration, shows current corners of camera and wiimote bounds
        //Instantiate(myPrefab, new Vector2((0f * cameraWidth) - xOffset, (1f * cameraHeight) - yOffset), Quaternion.identity);
        //Instantiate(myPrefab, new Vector2((0f * cameraWidth) - xOffset, (0f * cameraHeight) - yOffset), Quaternion.identity);
        //Instantiate(myPrefab, new Vector2((1f * cameraWidth) - xOffset, (1f * cameraHeight) - yOffset), Quaternion.identity);
        //Instantiate(myPrefab, new Vector2((1f * cameraWidth) - xOffset, (0f * cameraHeight) - yOffset), Quaternion.identity);

        // Setting up Wiimote. Uses mouse location if none are found.
        WiimoteManager.FindWiimotes();
        if (WiimoteManager.Wiimotes.Count != 0)
        {
            Debug.Log("Wiimote found!");
            usingMouse = false;
            wiimote1 = WiimoteManager.Wiimotes[0];
            wiimote1.SetupIRCamera(IRDataType.BASIC);
        } 
        else 
        {
            Debug.Log("No Wiimotes found.");
            usingMouse = true;
        }

        if (myButton != null)
        {
            buttonCollider = myButton.GetComponent<BoxCollider2D>();
        }
        
    }



    // Update is called once per frame
    void Update()
    {
        /**
        if (Input.GetMouseButtonDown(0))
        {
            P1_Popping = true;
            P1_Grabbing = true;
        } else
        {
            P1_Popping = false;
            P1_Grabbing = false;
        }
        **/

        if (usingMouse == false)
        {
            //Reading Wiimote data
            if (!WiimoteManager.HasWiimote()) { return; }
            wiimote1 = WiimoteManager.Wiimotes[0];
            int ret;
            do
            {
                ret = wiimote1.ReadWiimoteData();
            } while (ret > 0);

            // Sets IR position: pointer[0] is x, pointer[1] is y
            float[] pointer = wiimote1.Ir.GetPointingPosition();
            P1_IRPosition = new Vector2((pointer[0] * cameraWidth) - xOffset, (pointer[1] * cameraHeight) - yOffset);

            //Handles button presses
            if (wiimote1.Button.a == true)
            {
                if (wiimote1.Button.b == true)
                {
                    P1_Grabbing = true;
                    P1_Popping = false;

                }
                else
                {
                    P1_Popping = true;
                    P1_Grabbing = false;

                }
            }
            else
            {
                P1_Popping = false;
                P1_Grabbing = false;
            }


            if (myButton != null)
            {
                if (buttonCollider.bounds.Contains(P1_CursorPosition))
                {
                    if (myButton.GetComponent<Animator>() != null)
                    {
                        myButton.GetComponent<Animator>().Play("Hover");
                    }
                    
                    if (wiimote1.Button.a == true)
                    {
                        if (myButton.GetComponent<Animator>() != null)
                        {
                            myButton.GetComponent<Animator>().Play("Pressed");
                        }
                        myButton.GetComponent<Button>().onClick.Invoke();
                    }
                }
            } else
            {

            }

            if (Input.GetKeyDown("m"))
            {
                if (usingMouse)
                {
                    WiimoteManager.FindWiimotes();
                    if (WiimoteManager.Wiimotes.Count != 0)
                    {
                        Debug.Log("Wiimote found!");
                        usingMouse = false;
                        wiimote1 = WiimoteManager.Wiimotes[0];
                        wiimote1.SetupIRCamera(IRDataType.BASIC);
                    }
                    usingMouse = false;
                } else
                {
                    usingMouse = true;
                }
            }
        }

        else if (usingMouse == true)
        {
            // Gets mouse and instead uses mouse position to guide the cursor
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            P1_IRPosition = new Vector2(mousePos.x, mousePos.y);
        }
            /**
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.GetMouseButtonDown(1))
                {
                    P1_Grabbing = true;
                }
                else
                {
                    P1_Popping = true;
                }
            }
        
        }

        if (P1_Popping)
        {
            CursorPopBubble();
        } 
        else if (P1_Grabbing)
        {
            CursorHoldTrash();
        }
        **/


        // Final movement of IR pointer and cursor
        P1_IR.transform.position = P1_IRPosition;
        P1_CursorPosition = Vector2.Lerp(P1_CursorPosition, P1_IRPosition, Time.deltaTime * moveSpeed);
        P1_Cursor.transform.position = P1_CursorPosition;

        if(popTimerRunning)
        {
            popTimer -= Time.deltaTime;
        }
       
    }


    public void CursorActive()
    {
        if (isBeingAltered == false)
        {
            P1_Cursor.GetComponentInChildren<SpriteRenderer>().sprite = cursorActive;
        }
    }
    public void CursorHoldTrash()
    {
        if (isBeingAltered == false)
        {
            P1_Cursor.GetComponentInChildren<SpriteRenderer>().sprite = cursorHoldTrash;
        }
    }
    public void CursorHover()
    {
        if (isBeingAltered == false)
        {
            P1_Cursor.GetComponentInChildren<SpriteRenderer>().sprite = cursorHover;
        }
    }
    public void CursorPopBubble()
    {
        if (isBeingAltered == false)
        {
            P1_Cursor.GetComponentInChildren<SpriteRenderer>().sprite = cursorPopBubble;
        }
    }

}
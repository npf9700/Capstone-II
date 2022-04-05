using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using WiimoteApi;

public class PointerManager : MonoBehaviour
{

    private Quaternion initial_rotation;
    private Wiimote wiimote;
    public GameObject Pointer_P1;
    //public float oldPos;
    public Camera cam;
    private float cameraHeight;
    private float cameraWidth;
    public Vector2 NewPosition;


    public Color pointRed = Color.red;
    public Color pointBlue = Color.blue;
    //public Color pointGreen = green;


    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;

        // Instantiate at position (0, 0) and zero rotation.
        //GameObject myPointer = Instantiate(Pointer_P1, new Vector2(0, 0), Quaternion.identity);
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
    }



    // Update is called once per frame
    void Update()
    {
        if (!WiimoteManager.HasWiimote()) { return; }

        wiimote = WiimoteManager.Wiimotes[0];
        
        int ret;
        do
        {
            ret = wiimote.ReadWiimoteData();
        } while (ret > 0);


        //pointer[0] is x position, pointer[1] is y position
        float[] pointer = wiimote.Ir.GetPointingPosition();

        /**
        if (pointer[0] != oldPos)
        {
            Debug.Log("Changed!");
        }
        **/

        //Pointer_P1.transform.position = new Vector2( 0+Mathf.Round(pointer[0]*cameraWidth), 0+Mathf.Round(pointer[1]*cameraHeight) );
        //Instantiate(Pointer_P1, new Vector2((Mathf.Round(pointer[0] * cameraWidth) - cameraWidth/2) , (Mathf.Round(pointer[1] * cameraHeight)-cameraHeight/2) ), Quaternion.identity);
        //Instantiate(Pointer_P1, new Vector2( (pointer[0]*12)-6, (pointer[1]*12)-6 ), Quaternion.identity);

        NewPosition = new Vector2 ( (pointer[0] * 10) + (cameraWidth/4), (pointer[1] * 10) - (cameraHeight) ) ;

        Pointer_P1.transform.position = NewPosition;

        //Instantiate(Pointer_P1, NewPosition, Quaternion.identity);


        //if "A" pressed, set hue to ... . if "A and B" pressed, set hue to ... . Else set hue to normal
        if (wiimote.Button.a == true) {

            if (wiimote.Button.b == true)
            {
                //Pointer_P1.GetComponent<Renderer>().material.color = Color.green;
                Debug.Log("Trash Move Input!");
            }
            else
            {
                //Pointer_P1.GetComponent<Renderer>().material.color = pointRed;
                Debug.Log("Bubble Pop Input!");
            }

        } else {
            //Pointer_P1.GetComponent<SpriteRenderer>().color = pointRed;
            Debug.Log("No Input!");
        }

        /**
        model.a.enabled = wiimote.Button.a;
        model.b.enabled = wiimote.Button.b;
        model.plus.enabled = wiimote.Button.plus;
        **/

    }
}
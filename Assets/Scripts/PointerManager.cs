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

    //public Color pointRed = red;
    //public Color pointBlue = blue;
    //public Color pointGreen = green;


    // Start is called before the first frame update
    void Start()
    {
        // Instantiate at position (0, 0) and zero rotation.
        Instantiate(Pointer_P1, new Vector2(0, 0), Quaternion.identity);

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
        Pointer_P1.transform.position = new Vector2(pointer[0], pointer[1]);

        //if "A" pressed, set hue to ... . if "A and B" pressed, set hue to ... . Else set hue to normal
        if (wiimote.Button.a == true) {

            if (wiimote.Button.b == true)
            {
                Pointer_P1.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                Pointer_P1.GetComponent<Renderer>().material.color = Color.blue;
            }

        } else {
            Pointer_P1.GetComponent<Renderer>().material.color = Color.red;
        }

        /**
        model.a.enabled = wiimote.Button.a;
        model.b.enabled = wiimote.Button.b;
        model.plus.enabled = wiimote.Button.plus;
        **/

    }
}
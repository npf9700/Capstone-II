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
    private Wiimote wiimote;
    public GameObject Pointer_P1;
    public GameObject myPrefab;
    public Vector2 pointerPosition;



    // Start is called before the first frame update
    void Start()
    {
        //Setting up camera values
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        xOffset = cameraWidth / 2;
        yOffset = cameraHeight / 2;
        Debug.Log("camera width and height: " + cameraWidth + ", " + cameraHeight);

        //Bounds Calibration
        Instantiate(myPrefab, new Vector2((0f * cameraWidth) - xOffset, (1f  *cameraHeight) - yOffset), Quaternion.identity);
        Instantiate(myPrefab, new Vector2((0f * cameraWidth) - xOffset, (0f * cameraHeight) - yOffset), Quaternion.identity);
        Instantiate(myPrefab, new Vector2((1f * cameraWidth) - xOffset, (1f * cameraHeight) - yOffset), Quaternion.identity);
        Instantiate(myPrefab, new Vector2((1f * cameraWidth) - xOffset, (0f * cameraHeight) - yOffset), Quaternion.identity);

        //Setting up Wiimote
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
    }



    // Update is called once per frame
    void Update()
    {
        //Reading Wiimote data
        if (!WiimoteManager.HasWiimote()) { return; }
        wiimote = WiimoteManager.Wiimotes[0];
        int ret;
        do
        {
            ret = wiimote.ReadWiimoteData();
        } while (ret > 0);

        //Getting pointer position: pointer[0] is x position, pointer[1] is y position
        float[] pointer = wiimote.Ir.GetPointingPosition();

        //Sets position of pointer to IR pointer location. The float is multiplied across the width/height of the camera. The additional numbers are offsets to center it.
        pointerPosition = new Vector2 ( (pointer[0] * cameraWidth) - xOffset, (pointer[1] * cameraHeight) - yOffset);
        Pointer_P1.transform.position = pointerPosition;

        //Handles button presses
        if (wiimote.Button.a == true) {
            if (wiimote.Button.b == true)
            {
                //Call to Move Trash
                //Check Accelerometer?
            }
            else
            {
                //Call to Pop Bubble
            }
        } else {
            //Nothing
        }
    }

}
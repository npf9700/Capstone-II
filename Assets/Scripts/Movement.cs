using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 speed;
    Camera cam;
    float cameraWidth;
    float cameraHeight;
    public Vector3 currentPos;
    SpawnManager spm;
    // Start is called before the first frame update
    void Start()
    {
        spm = GameObject.FindObjectOfType(typeof(SpawnManager)) as SpawnManager;
        cam = Camera.main;
        cameraHeight = cam.orthographicSize * 2f;
        cameraWidth = cameraHeight * cam.aspect;
        speed = new Vector3(0f, Random.Range(0f, cameraHeight)) * Time.deltaTime;
        speed = Vector3.ClampMagnitude(speed, 0.0009f);
        currentPos = new Vector3(Random.Range(cam.transform.position.x - cameraWidth / 2, cam.transform.position.x + cameraWidth / 2), cam.transform.position.y - cameraHeight / 2 - 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //spawned boolean is set to true after initial call to FloatUp() method in SpawnManager.cs
        if(spm.spawned)
        {
            FloatUp();
        }
    }


   public void FloatUp()
   {
        currentPos.y += speed.y; //change y coordinate based on speed
        transform.position = currentPos;
   }

   void OnMouseDown()
   {
        //if (this.gameObject != null)
        //{
        //    spm.Despawn(this.gameObject);
        //}

        Debug.Log("Hi!");
      
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("BackToTitle", gameObject);
        AkSoundEngine.PostEvent("TutorialStart", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopTutorialMusic()
    {
        AkSoundEngine.PostEvent("TutorialStop", gameObject);
    }
}

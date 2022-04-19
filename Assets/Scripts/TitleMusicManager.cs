using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("BackToTitle", gameObject);
        AkSoundEngine.PostEvent("TitleStart", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopTitleMusic()
    {
        AkSoundEngine.PostEvent("TitleStop", gameObject);
    }
}

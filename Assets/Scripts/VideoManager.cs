using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public RawImage intro;
    public RawImage tutorial;
    public VideoPlayer introPlayer;
    public VideoPlayer tutorialPlayer;
    private List<VideoPlayer> videos;
    public SceneLoader sceneLdr;
    private int vidIndex;
    private float start;
    //public AudioSource audioPlayer;

    public TutorialMusicManager tutMusicMgr;

    void Awake()
    {
        //Sets initial start time
        start = Time.time;
        videos = new List<VideoPlayer>();
        //Resets the alpha levels to show intro and not tutorial
        intro.color = new Color(1f, 1f, 1f, 1f);
        tutorial.color = new Color(1f, 1f, 1f, 0f);
        videos.Add(introPlayer);
        videos.Add(tutorialPlayer);

        //tutorialPlayer.EnableAudioTrack(0, true);
        //tutorialPlayer.SetTargetAudioSource(0, audioPlayer);
        //tutorialPlayer.controlledAudioTrackCount = 1;

        //videos.Add(tutorialPlayer);
        vidIndex = 0;
        StartCoroutine(PlayNextVid());//Recursively calls PlayNextVid
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PlayNextVid()
    {
        //Debug.Log("vidIndex: " + vidIndex);
        videos[vidIndex].Play();
        while (Time.time - start < videos[vidIndex].length)
        {
            yield return null;
        }
        vidIndex++;
        tutorial.color = new Color(1f, 1f, 1f, 1f);
        intro.color = new Color(1f, 1f, 1f, 0f);
        start = Time.time;
        if (vidIndex < 2)
        {
            StartCoroutine(PlayNextVid());
        }
        else
        {
            tutMusicMgr.StopTutorialMusic();
            sceneLdr.GetComponent<SceneLoader>().LoadNextScene(0f);
        }
        yield return null;
        
    }

    public void StartNextVid()
    {
        vidIndex++;
        tutorial.color = new Color(1f, 1f, 1f, 1f);
        intro.color = new Color(1f, 1f, 1f, 0f);
        if (vidIndex == 2)
        {
            tutMusicMgr.StopTutorialMusic();
            sceneLdr.GetComponent<SceneLoader>().LoadNextScene(1.5f);
        }
        else
        {
            StartCoroutine(PlayNextVid());
        }
    }

}

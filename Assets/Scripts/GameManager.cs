using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    //Fields
    private int playerScore;
    public Text scoreText;
    private int trashNotCollected;
    public GameObject oilSlick;
    private Vector2 oilPos;


    public int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        trashNotCollected = 0;
        scoreText.text = "Score: " + playerScore;
        oilPos = oilSlick.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementScore(int scoreAdd)
    {
        playerScore += scoreAdd;
        scoreText.text = "Score: " + playerScore;
    }

    public void DecrementScore(int decScore)
    {
        playerScore -= decScore;
        scoreText.text = "Score: " + playerScore;
    }

    public void TrashNotCaught()
    {
        trashNotCollected++;
        if(trashNotCollected == 1)
        {
            oilPos.y -= 0.5f;
        }
        else if(trashNotCollected % 2 == 1)
        {
            oilPos.y -= 0.75f;
        }
        oilSlick.transform.position = oilPos;
    }

    //Implement collision detection with oil slick to check if 
    //GameObjects should be interactable
}

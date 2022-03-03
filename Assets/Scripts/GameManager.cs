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


    public int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        scoreText.text = "Score: " + playerScore;
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
}

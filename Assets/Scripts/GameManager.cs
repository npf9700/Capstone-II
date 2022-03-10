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
    public SpriteRenderer trashRend;
    public SpriteRenderer oilRend;
    private float halfTrashHeight;
    private float halfTrashWidth;
    private float halfOilHeight;
    private float halfOilWidth;
    private Rect oilRect;

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
        halfTrashHeight = trashRend.bounds.size.x / 2;
        halfOilHeight = oilRend.bounds.size.y / 2;
        halfTrashWidth = trashRend.bounds.size.y / 2;
        halfOilWidth = oilRend.bounds.size.x / 2;
        oilRect = new Rect(oilPos.x - halfOilWidth, oilPos.y - halfOilHeight, oilRend.bounds.size.x, oilRend.bounds.size.y);
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

    //Keeps track of the trash that falls to the bottom and moves the oil box 
    //farther down in accordance with the amount of trash
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

    public bool IsBehindOilSlick()
    {
        //Getting mouse position in world
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //Updating the oil Rectangle
        oilRect = new Rect(oilPos.x - halfOilWidth, oilPos.y - halfOilHeight, oilRend.bounds.size.x, oilRend.bounds.size.y);

        return oilRect.Contains(mousePos);
    }
}

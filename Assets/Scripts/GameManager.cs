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
    //private int ammo;
    public Slider ammoSlider;
    public Canvas GUICanvas;
    public Text ammoText;
    //Public timer value
    public float currentTime;
    public float startingTime;
    public Text countdownText;
    //modals
    public GameObject gameOverModal;

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
       // ammo = 3;
        GUICanvas = GameObject.Find("GUICanvas").GetComponent<Canvas>();
        ammoSlider = GUICanvas.GetComponentInChildren<Slider>();
        ammoText = ammoSlider.GetComponentInChildren<Text>();
        ammoText.text = ammoSlider.value.ToString();

        currentTime = startingTime;
        //Time.timeScale = 1;
        gameOverModal.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //timer script
        if (Mathf.RoundToInt(currentTime) == 0)
        {
            TriggerGameOver();
        } else
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = Mathf.RoundToInt(currentTime).ToString();
        }
    }

    public void IncrementScore(int scoreAdd)
    {
        playerScore += scoreAdd;
        scoreText.text = "Score: " + playerScore;

    }

    public void DecrementScore(int decScore)
    {
        if (playerScore <= 0 || (playerScore - decScore) <= 0) //prevents score from going negative
        {
            playerScore = 0;
            scoreText.text = "Score: " + playerScore;
            return;
        }
        else
        {
            playerScore -= decScore;
            scoreText.text = "Score: " + playerScore;
        }
    }

    //Keeps track of the trash that falls to the bottom and moves the oil box 
    //farther down in accordance with the amount of trash
    public void TrashNotCaught()
    {
        trashNotCollected++;
        AkSoundEngine.SetRTPCValue("TrashFallen", trashNotCollected);
        if(trashNotCollected == 1)
        {
            oilPos.y -= 0.5f;
        }
        else if(trashNotCollected % 2 == 1 && trashNotCollected < 28)
        {
            oilPos.y -= 0.75f;
        } else if(trashNotCollected == 28)
        {
            TriggerGameOver();
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



    public void TriggerGameOver()
    {
        //Show Game Over Modal
        //Reset Scene?
        //Clicking button sends back to title screen
        Debug.Log("GAME OVER!");
        gameOverModal.SetActive(true);

        //Time.timeScale = 0;
        //Application.LoadLevel(Application.loadedLevel);
        //LoadStartScene();
    }


}

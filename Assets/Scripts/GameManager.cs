using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    //Fields
    private int playerScore;
    public TMP_Text scoreText;
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
    public TMP_Text ammoText;
    //Public timer value
    public float currentTime;
    public float startingTime;
    public TMP_Text countdownText;
    //modals
    public GameObject gameOverModal;
    private bool isGameOver;
    public RawImage colorBackGround;
    public RawImage deadBackGround;
    public SpawnManager spm;
    public int numAnimalsSaved;
    public TMP_Text finalScore;
    public TMP_Text animalsSavedText;
    public TMP_Text noAmmoText;
    public TMP_Text trashRemovedText;
    public int trashRemoved;
    public int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.SetRTPCValue("TrashFallen", 0);
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
        ammoText = ammoSlider.GetComponentInChildren<TMP_Text>();
        ammoText.text = ammoSlider.value.ToString();
        noAmmoText = GameObject.FindGameObjectWithTag("NoAmmoText").GetComponent<TMP_Text>();
        trashRemovedText = GameObject.FindGameObjectWithTag("TrashRemoved").GetComponent<TMP_Text>();
        isGameOver = false;
        currentTime = startingTime;
        //Time.timeScale = 1;
        gameOverModal.SetActive(false);
        deadBackGround.color = new Color(1, 1, 1, 0);
        colorBackGround.color = new Color(1, 1, 1, 1);
        spm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        
        //ammoHandleArea = ammoSlider.GetComponentInChildren<HandleSlideArea>();

    }

    // Update is called once per frame
    void Update()
    {
        //timer script
        if (Mathf.RoundToInt(currentTime) == 0 && isGameOver == false)
        {
            TriggerGameOver();
            isGameOver = true;
            CancelInvoke("spm.IncreaseSpawnTime"); //just in case
        } else if(isGameOver == false)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = Mathf.RoundToInt(currentTime).ToString();
        }
        if (ammoSlider.value == 0) //hide ammo slider handle if ammo value is zero
        {
            noAmmoText.text = "Remove Trash to Raise Ammo!";
            ammoSlider.handleRect.GetComponent<Image>().color = new Color(0, 0, 0, 0f);
        }
        else
        {
            noAmmoText.text = "";
            ammoSlider.handleRect.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
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
        if(trashNotCollected < 28)
        {
            float backAlpha = colorBackGround.color.a;
            backAlpha -= 0.07f;
            colorBackGround.color = new Color(1, 1, 1, backAlpha);
            float deadAlpha = deadBackGround.color.a;
            deadAlpha += 0.07f;
            deadBackGround.color = new Color(1, 1, 1, deadAlpha);
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
        finalScore = GameObject.FindGameObjectWithTag("FinalScore").GetComponent<TMP_Text>();
        finalScore.text = "Final Score: " + playerScore;
        animalsSavedText = GameObject.FindGameObjectWithTag("NumSaved").GetComponent<TMP_Text>();
        animalsSavedText.text = "You rescued " + numAnimalsSaved + " animals!";
        trashRemovedText.text = "You removed " + trashRemoved + " pieces of trash!";
        AkSoundEngine.PostEvent("GameOver", gameObject);

        //Time.timeScale = 0;
        //Application.LoadLevel(Application.loadedLevel);
        //LoadStartScene();
    }

    public void StopAllMusic()
    {
        AkSoundEngine.PostEvent("BackToTitle", gameObject);
    }


}

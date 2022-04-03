using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
    This class controls the game over UI
*/
public class GameUI : MonoBehaviour
{

    public Image fadePlane;

    public GameObject gameOverUI;
    
    // The banner which shows at the begining of each wave
    public RectTransform waveBanner;
    public Text waveTitle;

    // Display Score
    public Text scoreUI;
    public Text gameoverScoreUI;

    // Set the Pivot to the leftest, by editing scale to show the health bar animation
    public RectTransform healthBar; 

    Spawner spawner;
    Player player;

    // Initial parameter before the Start
    void Awake()
    {   
        spawner = FindObjectOfType<Spawner>();
        spawner.OnNewWave += OnNewWave;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        player.OnDeath += OnGameOver;
    }   

    void Update()
    {   
        // could get the 000010 in thge score board
        scoreUI.text = ScoreBoard.score.ToString("D6");
        
        float healthPercent = 0;
        if(player != null){
            healthPercent = player.health / player.initialHealth;
        }
        // Changing the health bar's scale for health deduction effect
        healthBar.localScale = new Vector3(healthPercent, 1, 1);
    }

    // Show the game wave on the banner
    void OnNewWave(int waveNumber)
    {   
        // Display the specific wave on the banner
        string[] numbers = {"One", "Two", "Three", "Four", "Five"};
        waveTitle.text = "Wave " + numbers[waveNumber - 1];

        StartCoroutine(BannerAnimation());
    }

    // Show the game over UI once the player died
    void OnGameOver(){
        // Get the fade out plane's color
        // Set the Alpha to 1 to avoid invisible at the final
        Color imageClor = fadePlane.color;
        Color fadeOutColor = new Color(imageClor.r, imageClor.g, imageClor.b, 1f);

        StartCoroutine(Fade(Color.clear, fadeOutColor, 1));

        gameoverScoreUI.text = scoreUI.text;
        scoreUI.gameObject.SetActive(false);
        healthBar.transform.parent.gameObject.SetActive(false);

        gameOverUI.SetActive(true);
        Cursor.visible = true; // Show the cursor
    }


    // The uplift animation of the new wave banner
    IEnumerator BannerAnimation()
    {   
        float delayTime = 1f;
        float speed = 3f; // Animation speed
        float animatePercent = 0;

        int dir = 1; // The banner goes up (-1) then go down (-1)

        float endDelayTime = Time.time + 1 / speed + delayTime;

        while(animatePercent >=0)
        {
            animatePercent += Time.deltaTime * speed * dir;
            if(animatePercent >= 1)
            {
                animatePercent = 1;
                if(Time.time > endDelayTime)
                {
                    dir = -1;
                }
            }

            waveBanner.anchoredPosition = Vector2.up * Mathf.Lerp(-170,45, animatePercent);
            yield return null;
        }
    }

    // The fade animation of the game over UI
    IEnumerator Fade(Color from, Color to, float time){
        float speed = 1 / time;
        float percent = 0;

        while(percent < 1){
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    // Start a new game
    public void StartNewGame(){
        Application.LoadLevel("Game");
    }

    // Back to game's main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

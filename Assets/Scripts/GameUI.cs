using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public Image fadePlane;

    public GameObject gameOverUI;
    
    //The banner which shows at the begining of each wave
    public RectTransform waveBanner;
    public Text waveTitle;

    Spawner spawner;

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        spawner.OnNewWave += OnNewWave;
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Player>().OnDeath += OnGameOver;
    }   

    void OnNewWave(int waveNumber)
    {   
        // Display the specific wave on the banner
        string[] numbers = {"One", "Two", "Three", "Four", "Five"};
        waveTitle.text = "Wave " + numbers[waveNumber - 1];

        StartCoroutine(BannerAnimation());
    }

    void OnGameOver(){
        // Get the fade out plane's color
        // Set the Alpha to 1 to avoid invisible at the final
        Color imageClor = fadePlane.color;
        Color fadeOutColor = new Color(imageClor.r, imageClor.g, imageClor.b, 1f);

        StartCoroutine(Fade(Color.clear, fadeOutColor, 1));
        gameOverUI.SetActive(true);
        Cursor.visible = true; // Show the cursor
    }
    
    // // The uplift animation of the new wave banner
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

    // UI Input
    public void StartNewGame(){
        Application.LoadLevel("Game");
    }
}

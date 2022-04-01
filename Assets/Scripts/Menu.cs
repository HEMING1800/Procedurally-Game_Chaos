using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
    This class controls the menu UI
*/
public class Menu : MonoBehaviour
{   
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Play the game once press the play button
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    // Quit the game once press the quit button
    public void Quit()
    {
        Application.Quit();
    }
}

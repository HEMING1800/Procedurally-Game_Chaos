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
    
    // Enter the gun generation scene once press the play button
    public void GenerateGunScene()
    {
        SceneManager.LoadScene("WeaponGenerator");
    }

    // Quit the game once press the quit button
    public void Quit()
    {
        Application.Quit();
    }
}

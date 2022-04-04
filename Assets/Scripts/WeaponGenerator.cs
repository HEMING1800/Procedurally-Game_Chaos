using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponGenerator : MonoBehaviour
{   
    public List<GameObject> bodies; // Gun body parts
    public List<GameObject> barrels; // Gun barrel parts
    public List<GameObject> stocks; // Gun stock parts
    public List<GameObject> scopes; // Gun scope parts
    public List<GameObject> magazines; // Gun magazine parts
    public List<GameObject> grips; // Gun grip parts

    public GameObject generatedBody; // Generated gun

    // Update is called once per frame
    void Update()
    {   
        
    }

    public void GenerateWeapon()
    {
        // Get a random body part, and generate it in the game
        GameObject randomBody = GetRandomPart(bodies);
        generatedBody = Instantiate(randomBody, Vector3.zero, Quaternion.identity);
        WeaponBody gunBody = generatedBody.GetComponent<WeaponBody>();

        // Generate each weapon parts randomly from the list
        SpawnWeaponPart(barrels, gunBody.barrelSocket);
        SpawnWeaponPart(stocks, gunBody.stockSocket);
        SpawnWeaponPart(scopes, gunBody.scopeSocket);
        SpawnWeaponPart(magazines, gunBody.magazineSocket);
        SpawnWeaponPart(grips, gunBody.gripSocket);
    }

    // Get the random element from the list
    GameObject GetRandomPart(List<GameObject> partList)
    {
        int randomNumber = Random.Range(0, partList.Count);
        return partList[randomNumber];
    }
    
    // Get a random guns' part, and generate in the each socket position
    void SpawnWeaponPart(List<GameObject> parts, Transform socket)
    {
        GameObject randomPart = GetRandomPart(parts);
        GameObject generatedPart = Instantiate(randomPart, socket.transform.position, socket.transform.rotation);

        // Set the parent 
        generatedPart.transform.parent = socket; 
    }

    // Enter game scene once press the start button
    public void Play()
    {   
        if(generatedBody){
            SceneManager.LoadScene("Game");
        }
    }

    // Back to game's main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Gun Generation
    public void GunGeneration()
    {
        // Destroy the previous generated gun
        if(generatedBody){
            Destroy(generatedBody);
        }
        GenerateWeapon();
    }
}

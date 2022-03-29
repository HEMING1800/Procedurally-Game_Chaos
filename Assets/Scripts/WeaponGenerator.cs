using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{   
    public List<GameObject> bodies; // Gun body parts
    public List<GameObject> barrels; // Gun barrel parts
    public List<GameObject> stocks; // Gun stock parts
    public List<GameObject> scopes; // Gun scope parts
    public List<GameObject> magazines; // Gun magazine parts
    public List<GameObject> grips; // Gun grip parts

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // For the manuel testing. generate weapon if press the SPACE
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateWeapon();
        }
    }

    void GenerateWeapon()
    {
        // Get a random body part, and generate it in the game
        GameObject randomBody = GetRandomPart(bodies);
        GameObject generatedBody = Instantiate(randomBody, Vector3.zero, Quaternion.identity);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{

    //reference to the camera the player was last using
    public GameObject cameraGame;
    public GameObject cameraInventory;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            Debug.Log("OpenInventory");
            //cameraGame = Camera.main;
            //cameraGame.SetActive(false);
            //cameraInventory.SetActive(true);
            cameraGame.SetActive(!cameraGame.activeInHierarchy);
            cameraInventory.SetActive(!cameraInventory.activeInHierarchy);
            //cameraGame.enabled = !cameraGame.enabled;
            //cameraInventory.enabled = !cameraInventory.enabled;

        }
        
    }

    //when player presses the inventory key
    //switch camera to inventory camera
    //enable switchItem functionality

    //when inventory key or esc is pressed
    //switch to previous camera
}

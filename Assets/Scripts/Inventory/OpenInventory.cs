using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{

    //reference to the camera the player was last using
    public GameObject cameraGame;
    public GameObject cameraInventory;
    public GameObject manager;
    private Camera[] cameras;
    private GameObject init_cam;

    public void Start()
    {
        GameObject camMaster = GameObject.Find("CameraMaster");
        cameras = camMaster.GetComponent<CameraArray>().cameras;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            

                //enable or disable inventory camera
                cameraInventory.SetActive(!cameraInventory.activeInHierarchy);


                //enable or disable player input
                if (this.gameObject.GetComponent<TankControls>().inputEnabled)
                {
                    
                    //set the initial cam
                    init_cam = Camera.main.gameObject;

                    //disable all active camera angles
                    for (int i = 0; i < cameras.Length; i++)
                    {
                        cameras[i].gameObject.SetActive(false);
                    }
                    this.gameObject.GetComponent<TankControls>().inputEnabled = false;
                    KillerBehavior.canMove = false;
                    GameObject.Find("InventoryManager").GetComponent<InventorySpawner>().CheckSave();
                    //manager.SendMessage("UpdateInventory");
                }

                else
                {
                    //reactivate the initial camera
                    init_cam.SetActive(true);

                    this.gameObject.GetComponent<TankControls>().inputEnabled = true;
                    KillerBehavior.canMove = true;

                //reset inventory rotation
                transform.rotation = Quaternion.Euler(0, 0, 0);
                }
        }
        
    }

    //when player presses the inventory key
    //switch camera to inventory camera
    //enable switchItem functionality

    //when inventory key or esc is pressed
    //switch to previous camera
}

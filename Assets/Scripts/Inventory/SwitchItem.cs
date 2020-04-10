﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchItem : MonoBehaviour
{

    //private float speed = 5;
    // private float width = 4;
    //private float height = 7;
    
    //need a ref to the inventory manager
    public GameObject manager;

    //angle Variables
    InventorySpawner inventory;
    public GameObject equipItem;
    //speed of switch
    //public float RotationSpeed = 2.0f;

    //bool canSwitchL = true;
    //bool canSwitchR = true;
    private int inventoryIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //find the current angle between inventory items.
        inventory = gameObject.GetComponent<InventorySpawner>();
        equipItem = inventory.inventoryItems[inventoryIndex];
        //Debug.Log(inventoryIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {

            //transform.eulerAngles = new Vector3(xRotation, transform.eulerAngles.y, transform.eulerAngles.z);
            //this.transform.Rotate(0, RotationSpeed, 0);
            //iTween.RotateTo(manager, "y", inventory.inventorySlice, 1.0f);
            iTween.RotateAdd(manager, iTween.Hash("y", inventory.inventorySlice, "time", 1.0f));
            inventoryIndex++;
            if(inventoryIndex >= inventory.inventoryItems.Count)
            {
                inventoryIndex = 0;
            }
            equipItem = inventory.inventoryItems[inventoryIndex];
            Debug.Log(inventoryIndex);
            Debug.Log(equipItem);
        }
        if (Input.GetKey(KeyCode.O))
        {
            //transform.eulerAngles = new Vector3(xRotation1, transform.eulerAngles.y, transform.eulerAngles.z);
            iTween.RotateAdd(manager, iTween.Hash("y", -1 * inventory.inventorySlice, "time", 1.0f));
            inventoryIndex--;
            if (inventoryIndex < 0)
            {
                inventoryIndex = inventory.inventoryItems.Count;
            }
            equipItem = inventory.inventoryItems[inventoryIndex];
            Debug.Log(inventoryIndex);
            Debug.Log(equipItem);
        }
        
    }


    //transform.Rotate(Vector3.up* (RotationSpeed* Time.deltaTime ) )


    //function for when the user wants to change the selected item.
    //function requires the angle of the slice between items
    //if left
    public void ShiftLeft()
    {
        //rotate inventory manager by inventory slice over a lerp of time
    }
    
    
    //if right
    public void ShiftRight()
    {
        //StartCoroutine(Exhaustion());
    }

    //IEnumerator Exhaustion()
    //{
    //    Debug.Log("Started Coroutine at timestamp : " + Time.time);
    //    canSwitch = false;
    //    yield return new WaitForSeconds(5);
    //    Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    //    canSwitch = true;
    //}
    //rotate by angle

    //only change the object if switch object bool is true.
    //make the lerp amount negative or positive depending on left or 
    //right swtich
    //use same method of spawning an object
    //add p to angle where p is a lerp between one object and the next
    //make sure the angle isn't negative
    //timeCounter += Time.deltaTime;

    //foreach (GameObject slot in inventoryItems)
    //{
    //    float x = Mathf.Cos(timeCounter) * width;
    //    float y = 0;
    //    float z = Mathf.Sin(timeCounter) * height;

    //    slot.transform.position = new Vector3(x, y, z);
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpawner : MonoBehaviour
{
    //public arraya of inventory items.
    public List<GameObject> inventoryItems = new List<GameObject>();
    public int inventorySlice = 90;
    public GameObject manager;
    //public List<GameObject> currentInventory = new List<GameObject>();
    //orbitting objects
    //float timeCounter = 0;

    private GameObject item;

    

    // Start is called before the first frame update
    void Start()
    {
        //find amount of items in array
        //get desired angle by 360/array length

        inventorySlice = 360 / inventoryItems.Count;

        //transform with center of inventory circle
        Vector3 center = transform.position;

        float space = 1;

        //loop that places each item in the inventory
        foreach (GameObject slot in inventoryItems)
        {
            //start at 0.
            //spawn item with offset
            Vector3 pos = CirCircle(center, 3.0f, space);
            //update variable for determining the angle of hte next item
            //update angle for next item
            space += 1;
            //random rotation doesn't matter.
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            //save ref to item
            
            item = Instantiate(slot, pos, rot);
            item.transform.SetParent(manager.transform);
            //currentInventory.Add(item);
            //add item to list for ref.


        }
    }

    // Update is called once per frame
    void Update()
    {
       


    }


    Vector3 CirCircle(Vector3 center, float radius, float space)
    {
        //declare the angle of the object
        float ang = (inventorySlice * space) + 180;
        //set object location
        Vector3 pos;
        //find the run coordinate based on radius and angle
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        //find the rise coordinate based on radius and angle
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }



    
}

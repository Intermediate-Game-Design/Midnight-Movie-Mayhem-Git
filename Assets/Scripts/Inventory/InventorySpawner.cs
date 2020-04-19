using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpawner : MonoBehaviour
{
    //public array of inventory items.
    public List<GameObject> inventoryItems = new List<GameObject>();

    public int inventorySlice = 90;
    public GameObject manager;
    //public List<GameObject> currentInventory = new List<GameObject>();
    //orbitting objects
    //float timeCounter = 0;

    private GameObject item;

    //ALL POSSIBLE ITEMS MUST BE PLACED HERE BEFORE GAME IS RUN
    public GameObject crankHandle;
    public GameObject poolBall;
    public GameObject crowbar;
    public GameObject saw;
    public GameObject revolver;
    public GameObject VHS;
    public GameObject ticketBoothKey;
    public GameObject ExitGateKey;

    public int temp_size;
    public bool change = false; //defines if inventory should change



    // Start is called before the first frame update
    void Start()
    {
        //UpdateList();
    }
    private void Update()
    {
        //why is this here and not on a script on the ball itself?
        GameObject.FindWithTag("Pool Ball").transform.rotation = Quaternion.Euler(180, 0, 0);
    }
    public void CheckSave()
    {
        //int size = inventoryItems.Count;

        //this gets the state of all the possible items in the inventory.
        //false is not in inventory.

        InventoryState.IniReadValue("Items", "slot1");
        InventoryState.IniReadValue("Items", "slot2");
        InventoryState.IniReadValue("Items", "slot3");
        InventoryState.IniReadValue("Items", "slot4");
        InventoryState.IniReadValue("Items", "slot5");
        InventoryState.IniReadValue("Items", "slot6");
        InventoryState.IniReadValue("Items", "slot7");
        InventoryState.IniReadValue("Items", "slot8");

        //current state of possible pickup items.
    
        string sw = GameState.IniReadValue("Items", "Saw");    
        string pool_ball = GameState.IniReadValue("Items", "Pool Ball");
        string handle = GameState.IniReadValue("Items", "Crank Handle");
        string gun = GameState.IniReadValue("Items", "Revolver");

        //checks if pool ball has been picked up. If so, nothing. if not, add it to the inventory items.
        if (pool_ball == "true")
        {
            if (inventoryItems.Count == 0)
            {
                inventoryItems.Add(poolBall);
            }
            else
            {

                //Debug.Log("pool ball is true");
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (inventoryItems[i].name == "Pool Ball")
                    {
                        Debug.Log("pool ball already exists");
                        break;
                    }
                    else if (i == inventoryItems.Count-1 && inventoryItems[i].name != "Pool Ball")
                    {
                        inventoryItems.Add(poolBall); //find the object marked "poolBall"  
                        //if the item was just added.
                        change = true;
                    }
                }
            }
        }

        //checks if saw has been picked up. If so, nothing. if not, add it to the inventory items.
        if (sw == "true")
        {
                //Debug.Log("pool ball is true");
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (inventoryItems[i].name == "Saw")
                    {
                        Debug.Log("saw already exists");
                        break;
                    }
                    else if (i == inventoryItems.Count-1 && inventoryItems[i].name != "Saw")
                    {
                        inventoryItems.Add(saw); //find the object marked "poolBall"
                        Debug.Log("saw added");
                    //if the item was just added.
                    change = true;
                    }

                }
        }

        //checks if handle has been picked up. If so, nothing. if not, add it to the inventory items.
        if (handle == "true")
        {
            //Debug.Log("pool ball is true");
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].name == "CrankHandle")
                {
                    Debug.Log("handle already exists");
                    break;
                }
                else if (i == inventoryItems.Count - 1 && inventoryItems[i].name != "CrankHandle")
                {
                    inventoryItems.Add(crankHandle); //find the object marked "poolBall"
                    Debug.Log("handle added");
                    //if the item was just added.
                    change = true;
                }

            }
        }
        //checks if gun has been picked up. If so, nothing. if not, add it to the inventory items.
        if (gun == "true")
        {
            //Debug.Log("pool ball is true");
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].name == "Revolver")
                {
                    Debug.Log("revolver already exists");
                    break;
                }
                else if (i == inventoryItems.Count - 1 && inventoryItems[i].name != "Revolver")
                {
                    inventoryItems.Add(revolver); //find the object marked "poolBall"
                    Debug.Log("revolver added");
                    //if the item was just added.
                    change = true;
                }

            }
        }
        //if a new item was added, destroy all current children items in the inventory manager .
        if (change)
        {
            DestroyList();
        }
        //run spawning function
        UpdateList();
    }

    public void UpdateList()
    {
        if (change)
        {
            Debug.Log(inventoryItems.Count);


                //find amount of items in array
                //get desired angle by 360/array length
                if (inventoryItems.Count > 0)
                {
                    inventorySlice = 360 / inventoryItems.Count;
                }
                //transform with center of inventory circle
                Vector3 center = transform.position;

                float space = 1;


          

     
            foreach(GameObject slot in inventoryItems)
            {
                


                //start at 0.
                //spawn item with offset
                Vector3 pos = CirCircle(center, 3.0f, space);
                //update variable for determining the angle of the next item
                //update angle for next item
                space += 1;
                //random rotation doesn't matter.
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
                //save ref to item


                    item = Instantiate(slot, pos, rot);
                    item.transform.SetParent(manager.transform);
                    item.AddComponent<RotationScript>();

                
                //currentInventory.Add(item);
                //add item to list for ref.
            }
            change = false;
        }
        


    }   

    public void DestroyList()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
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

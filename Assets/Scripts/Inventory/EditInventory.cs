using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditInventory : MonoBehaviour
{
    //reference to current item 
    public GameObject inventory;
    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        //switch_items = inventory.GetComponent<SwitchItem>();
        //spawner = inventory.GetComponent<InventorySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            //AddItem(item);
        }
    }
    //add an item to inventory function
    void AddItem(GameObject newItem)
    {
        Debug.Log("Add item");
        //add item ref to current inventory
        //inventory.GetComponent<InventorySpawner>().AddItem(newItem);
        //inventory.GetComponent<InventorySpawner>().change = true;
        //inventory.GetComponent<InventorySpawner>().UpdateList();
        //set as new current item
        //equipped.equipItem = inventory.inventoryItems[inventory.inventoryItems.Count];
    }



    //remove item from inventory
    void RemoveItem(GameObject usedItem)
    {
        //find item with tag/name current inventory
        //for loop
        for (int i = 0; i <= inventory.GetComponent<InventorySpawner>().inventoryItems.Count; i++)
        {
            //check for matching name
            if (inventory.GetComponent<InventorySpawner>().inventoryItems[i] == usedItem)
            {
                //delete
                inventory.GetComponent<InventorySpawner>().inventoryItems.RemoveAt(i);

                //if none higher, then go lower.
                if (inventory.GetComponent<InventorySpawner>().inventoryItems.Count == i)
                {
                    //equipped.equipItem = inventory.inventoryItems[0];
                }
                //make current item the index after the removed object.
                //equipped.equipItem = inventory.inventoryItems[i + 1];
            }
        }



    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditInventory : MonoBehaviour
{
    //reference to current item
    SwitchItem equipped;
    InventorySpawner inventory;
    // Start is called before the first frame update
    void Start()
    {
        equipped = gameObject.GetComponent<SwitchItem>();
        inventory = gameObject.GetComponent<InventorySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //add an item to inventory function
    void AddItem(GameObject newItem)
    {
        //add item ref to current inventory
        inventory.inventoryItems.Add(newItem);
        //set as new current item
        equipped.equipItem = inventory.inventoryItems[inventory.inventoryItems.Count];
    }



    //remove item from inventory
    void RemoveItem(GameObject usedItem)
    {
        //find item with tag/name current inventory
            //for loop
            for(int i=0; i<= inventory.inventoryItems.Count; i++)
        {
            //check for matching name
            if (inventory.inventoryItems[i] == usedItem)
            {
                //delete
                inventory.inventoryItems.RemoveAt(i);
                
                //if none higher, then go lower.
                if (inventory.inventoryItems.Count == i)
                {
                    equipped.equipItem = inventory.inventoryItems[0];
                }
                //make current item the index after the removed object.
                equipped.equipItem = inventory.inventoryItems[i + 1];
            }
        }
            
        
        
    }

}

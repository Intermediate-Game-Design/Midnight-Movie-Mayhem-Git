using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

public class GameState : MonoBehaviour
{
    private static string path = "C:/Users/Josh Watson/Documents/UnityProjects/Midnight-Movie-Mayhem-Git/GameState.ini";

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section,
             string key, string def, StringBuilder retVal,
        int size, string filePath);

    /// <summary>
    /// Write Data to the INI File
    /// </summary>
    /// <PARAM name="Section"></PARAM>
    /// Section name
    /// <PARAM name="Key"></PARAM>
    /// Key Name
    /// <PARAM name="Value"></PARAM>
    /// Value Name
    public static void IniWriteValue(string Section, string Key, string Value)
    {
        WritePrivateProfileString(Section, Key, Value, path);
    }

    /// <summary>
    /// Read Data Value From the Ini File
    /// </summary>
    /// <PARAM name="Section"></PARAM>
    /// <PARAM name="Key"></PARAM>
    /// <PARAM name="Path"></PARAM>
    /// <returns></returns>
    public static string IniReadValue(string Section, string Key)
    {
        StringBuilder temp = new StringBuilder(255);
        //int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
        GetPrivateProfileString(Section, Key, "", temp, 255, path);
        return temp.ToString();

    }

    private void Start()
    {
        string side_door_to_inside = IniReadValue("Doors", "Side Door To Inside");
        string side_door_to_outside = IniReadValue("Doors", "Side Door To Outside");
        string office_to_storage_door = IniReadValue("Doors", "Office To Storage Door");
        string storage_to_office_door = IniReadValue("Doors", "Storage To Office Door");
        string emergency_door = IniReadValue("Doors", "EmergencyDoor");
        string shed_door = IniReadValue("Doors", "Shed");

        string pool_ball = IniReadValue("Items", "Pool Ball");
        string saw = IniReadValue("Items", "Saw");
        string cb = IniReadValue("Items", "Crowbar");
        string car = IniReadValue("Car", "car_state");

        if(side_door_to_outside == "unlocked" && GameObject.Find("SideDoorToOutside") != null)
        {
            Debug.Log("SideDoor Exists");
            GameObject.Find("SideDoorToOutside").GetComponent<ChangeRooms>().unlocked = false;
        }
        if(side_door_to_inside == "unlocked" && GameObject.Find("SideDoorToInside") != null)
        {
            Debug.Log("SideDoor Exists");
            GameObject.Find("SideDoorToInside").GetComponent<ChangeRooms>().locked = false;
        }


        if (office_to_storage_door == "unlocked" && GameObject.Find("OfficeToStorage") != null) 
        {
            GameObject.Find("OfficeToStorage").GetComponent<ChangeRooms>().unlocked = false;
        }
        if(storage_to_office_door == "unlocked" && GameObject.Find("StorageToOffice") != null) 
        {
            GameObject.Find("StorageToOffice").GetComponent<ChangeRooms>().locked = false;
        }
        if(emergency_door == "unlocked" && GameObject.Find("StairsPrompt") != null)
        {
            GameObject.Find("StairsPrompt").GetComponent<ChangeRooms>().locked = false;
        }
        if(shed_door == "unlocked" && GameObject.Find("ShedDoor") != null)
        {
            GameObject.Find("ShedDoor").GetComponent<ChangeRooms>().locked = false;
            GameObject.Find("ShedDoor").GetComponent<ChangeRooms>().unlocked = false;
        }





        //keep track of what items have been grabbed... don't allow an item to be grabbed more than once
        if(pool_ball == "true")
        {
            if (GameObject.Find("Text_PoolTable") != null)
            {
                Destroy(GameObject.Find("Text_PoolTable"));
                GameObject.Find("BlueBall").transform.position = new Vector3(0, 1000, 0);
            }
            if (GameObject.Find("specialcar") != null)
            {
                GameObject.Find("specialcar").GetComponent<DetermineTextObject>().TextObject = GameObject.Find("Text_CarUnlocked");
            }
           

        }
        if(saw == "true")
        {
            if (GameObject.Find("Saw") != null)
            {
                Destroy(GameObject.Find("Text_Saw"));
                Destroy(GameObject.Find("Saw"));
            }

        }
        if(cb == "true")
        {
            if (GameObject.Find("ShedDoor") != null)
            {
                GameObject obj = GameObject.Find("ShedDoor");
                obj.GetComponent<DetermineTextObject>().TextObject = GameObject.Find("Text_ShedUnlocked");

                if (shed_door == "unlocked")
                {
                    obj.GetComponent<ChangeRooms>().locked = false;
                    obj.GetComponent<ChangeRooms>().unlocked = false;
                }
                else
                {
                    obj.GetComponent<ChangeRooms>().locked = false;
                    obj.GetComponent<ChangeRooms>().unlocked = true;
                }
            }
            if (GameObject.Find("specialcar"))
            {
                Destroy(GameObject.Find("Text_CarUnlocked"));
                Destroy(GameObject.Find("Text_CarLocked"));
            }
            

        }

        
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        IniWriteValue("Items", "Crank Handle","true");
    //    }
    //    if (Input.GetKeyDown(KeyCode.Y))
    //    {
    //        IniWriteValue("Items", "Revolver","true");
    //    }
    //}
}

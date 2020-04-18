﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

public class InventoryState : MonoBehaviour
{
    private static string path = "C:/Users/Josh Watson/Documents/UnityProjects/Midnight-Movie-Mayhem-Git/Inventory.ini";

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
        
        
    }
}

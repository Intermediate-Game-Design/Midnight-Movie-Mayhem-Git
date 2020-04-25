﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(TW_Regular)), CanEditMultipleObjects]
[Serializable]
public class TW_Regular_Editor : Editor
{
    private static string[] PointerSymbols = { "None", "<", "_", "|", ">" };
    private TW_Regular TW_RegularScript;
  

    private void Awake()
    {
        TW_RegularScript = (TW_Regular)target;
    }

    private void MakePopup(SerializedObject obj)
    {
        TW_RegularScript.pointer = EditorGUILayout.Popup("Pointer symbol",TW_RegularScript.pointer, PointerSymbols, EditorStyles.popup);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SerializedObject SO = new SerializedObject(TW_RegularScript);
        MakePopup(SO);
    }
}
#endif

public class TW_Regular : MonoBehaviour {

    public bool LaunchOnStart = true;
    private int timeOut = 1;
    [HideInInspector]
    public int pointer=0;
    public string ORIGINAL_TEXT;
    public int string_length; 
    private float time = 0f;
    public int сharIndex = 0;
    private bool start = false;
    public static bool displayText;
    public GameObject callingObject = null;


    private bool go = false;
    private bool display_text = false;

    public GameObject Player;
    public GameObject InteractableParent;
    public GameObject InventoryManager;
    public static GameObject item_temp;
    public bool item;
    public bool action;

    //public DetermineTextObject determineTextObject;

    private List<int> n_l_list;
    private static string[] PointerSymbols = { "None", "<", "_", "|", ">" };
    private int charIndex;

    public string TEXT { get; private set; }

    void Start () {
        ORIGINAL_TEXT = gameObject.GetComponent<TextMeshProUGUI>().text;
        gameObject.GetComponent<TextMeshProUGUI>().text = "";
        if(Player == null)
        {
            Player = GameObject.Find("Player");
        }
        if(InteractableParent == null)
        {
            InteractableParent = GameObject.Find("Interactables");
        }
        if (LaunchOnStart)
        {
            StartTypewriter();
        }
    }
	
	void Update () {

        string_length = ORIGINAL_TEXT.Length;

        //for skipping text
        if (Input.GetKey(KeyCode.Z) && display_text)
        {
            go = true;
            gameObject.GetComponent<TextMeshProUGUI>().text = ORIGINAL_TEXT;
            charIndex = ORIGINAL_TEXT.Length + 1;
            AudioSource sound = InteractableParent.GetComponent<AudioSource>();
            sound.Stop();
        }

        //start the text
        if (start == true){
            display_text = true;
            NewLineCheck(ORIGINAL_TEXT);
        }

        //close the text
        if (go){

            //if it's not an item
            if (item == false && action == false && Input.GetKey(KeyCode.Space))
            {
                //Debug.Log("Space pressed");
                SkipTypewriter();
                go = false;


                Debug.Log(callingObject);
                if (callingObject.name == "specialcar")
                {
                    GameState.IniWriteValue("Items", "Crowbar", "true");
                    Destroy(GameObject.Find("Text_CarLocked"));
                    Destroy(GameObject.Find("Text_CarUnlocked"));
                    GameObject killer = GameObject.Find("KillerSpawner(Clone)").GetComponent<SpawnKiller>().killer;
                    GameObject[] points = GameObject.Find("EnemySpawnPoints").GetComponent<AIPatrol>().spawnPoints;
                    GameObject spawnPoint = points[1];

                    if (GameObject.FindWithTag("Killer") == null)
                    {
                        Instantiate(killer, spawnPoint.transform.position, spawnPoint.transform.rotation);
                        KillerBehavior.chasing = true;
                        //ai.GetComponent<KillerBehavior>().personalLastSighting = GameObject.Find("Player").transform.position;

                    }
                    else
                    {
                        KillerBehavior.chasing = true;
                    }
                }
            }

            //if it's an item, choose what to do with it
            if (Input.GetKey(KeyCode.Y)){
                if (item == true)
                {
                    Destroy(this.gameObject);
                    SkipTypewriter();
                    go = false;
                    //PlayPickupAnimation();
                    //AddToInventory();
                    if (callingObject.name == "BilliardsTable")
                    {
                        GameState.IniWriteValue("Items", "Pool Ball", "true");
                        InventoryState.IniWriteValue("Items", "slot1", "Pool Ball");

                        //GameObject item = GameObject.Find("Wrench");
                        //InventorySpawner.temp_item = "Wrench";
                        

                    }
                    if (callingObject.name == "SawTrigger")
                    {
                        GameState.IniWriteValue("Items", "Saw", "true");
                        Destroy(GameObject.Find("Saw"));
                        //InventoryState.IniWriteValue("Items","slot")
                    }
                    


                }
                if(action == true)
                {
                    SkipTypewriter();
                    go = false;
                    string level = callingObject.GetComponent<ChangeRooms>().level;
                    callingObject.GetComponent<ChangeRooms>().ChangeScene(level);
                    //PerformAction();
                }
            }
            if ((item || action) && Input.GetKey(KeyCode.N))
            {
                SkipTypewriter();
                go = false;
            }
        }
        
    }
  
    public void StartTypewriter()
    {
        if (GameObject.FindWithTag("Killer") != null)
        {
            KillerBehavior.canMove = false;
        }
        start = true;
        сharIndex = 0;
        time = 0f;
    }


    public void SkipTypewriter()
    {
        //Debug.Log("Closing text");
        сharIndex = 0;
        DetermineTextObject.displayText = false;
        Player.GetComponent<TankControls>().inputEnabled = true;
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "";
        Debug.Log(this.gameObject.GetComponent<TextMeshProUGUI>().text.ToString());
        display_text = false;
        start = false;
        DetermineTextObject.textWaitTimer = 1.5f;
        DetermineTextObject.go = true;

        if (callingObject != null)
        {
            if (callingObject.GetComponent<ChangeRooms>() != null)
            {
                if (callingObject.GetComponent<ChangeRooms>().unlocked == true)
                {
                    callingObject.GetComponent<ChangeRooms>().unlocked = false;

                    switch (callingObject.name)
                    {
                        case "SideDoorToOutside":
                            GameState.IniWriteValue("Doors", "Side Door To Outside", "unlocked");
                            GameState.IniWriteValue("Doors", "Side Door To Inside", "unlocked");
                            break;
                        case "OfficeToStorage":
                            GameState.IniWriteValue("Doors", "Office To Storage Door", "unlocked");
                            GameState.IniWriteValue("Doors", "Storage To Office Door", "unlocked");
                            break;
                        case "ShedDoor":
                            GameState.IniWriteValue("Doors", "Shed", "unlocked");
                            break;
                        default:
                            Debug.Log(callingObject.name);
                            break;
                    }
                }
            }
        }
        if (GameObject.FindWithTag("Killer") != null)
        {
            KillerBehavior.canMove = true;
        }
    }

    private void NewLineCheck(string S)
    {
        if (S.Contains("\n")){
            StartCoroutine(MakeTypewriterTextWithNewLine(S, GetPointerSymbol(), MakeList(S)));
        }
        else{
            StartCoroutine(MakeTypewriterText(S,GetPointerSymbol()));
        }
    }

    private IEnumerator MakeTypewriterText(string ORIGINAL, string POINTER)
    {
        if (!go)
        {
            start = false;
            if (сharIndex < ORIGINAL.Length + 1)
            {
                string emptyString = new string(' ', ORIGINAL.Length - POINTER.Length);
                string TEXT = ORIGINAL.Substring(0, сharIndex);
                if (сharIndex < ORIGINAL.Length) TEXT = TEXT + POINTER + emptyString.Substring(сharIndex);
                gameObject.GetComponent<TextMeshProUGUI>().text = TEXT;
                time += 1;
                AudioSource sound = InteractableParent.GetComponent<AudioSource>();
                if (!sound.isPlaying)
                {
                    sound.Play();
                }
                yield return new WaitForSeconds(0);
                CharIndexPlus();
                start = true;
                go = false;



            }
            else
            {
                go = true;
                AudioSource sound = InteractableParent.GetComponent<AudioSource>();
                sound.Stop();
            }
        }
    }

    private IEnumerator MakeTypewriterTextWithNewLine(string ORIGINAL, string POINTER, List<int> List)
    {
        start = false;
        if (сharIndex != ORIGINAL.Length + 1)
        {
            string emptyString = new string(' ', ORIGINAL.Length - POINTER.Length);
            string TEXT = ORIGINAL.Substring(0, сharIndex);
            if (сharIndex < ORIGINAL.Length) TEXT = TEXT + POINTER + emptyString.Substring(сharIndex);
            TEXT = InsertNewLine(TEXT, List);
            gameObject.GetComponent<TextMeshProUGUI>().text = TEXT;
            time += 1f;
            yield return new WaitForSeconds(0.01f);
            CharIndexPlus();
            start = true;
        }
    }

    private List<int> MakeList(string S)
    {
        n_l_list = new List<int>();
        for (int i = 0; i < S.Length; i++)
        {
            if (S[i] == '\n')
            {
                n_l_list.Add(i);
            }
        }
        return n_l_list;
    }

    private string InsertNewLine(string _TEXT, List<int> _List)
    {
        for (int index = 0; index < _List.Count; index++)
        {
            if (сharIndex - 1 < _List[index])
            {
                _TEXT = _TEXT.Insert(_List[index], "\n");
            }
        }
        return _TEXT;
    }

    private string GetPointerSymbol()
    {
        if (pointer == 0){
            return "";
        }
        else{
            return PointerSymbols[pointer];
        }
    }

    private void CharIndexPlus()
    {
        if (time == timeOut)
        {
            time = 0f;
            сharIndex += 1;
        }
    }
    
 

}

internal class yield
{
}
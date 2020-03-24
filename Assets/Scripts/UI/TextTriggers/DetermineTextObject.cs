using UnityEngine;
using System.Collections;

public class DetermineTextObject : MonoBehaviour
{
    public GameObject TextObject;
    public GameObject Player;

    public bool ready;

    public static bool displayText;
    public static float textWaitTimer = 0f;
    public static bool go = false;
    public static float maxDistance = 2.5f;
    public static Vector3 position0;
    public static Vector3 position1;
    public static Vector3 position2;
    public static Vector3 position3;
    public static Vector3 position4;
    public static Vector3 position5;
    public static Vector3 position6;
    public static Vector3 position7;
    public static string[] obj;
    public static RaycastHit[] hit;


    private void Update()
    {
       

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    go = true;
        //}
        if (go)
        {
            textWaitTimer -= Time.deltaTime;
        }
        //check if textWaitTimer is not 0
        if (textWaitTimer <= 0)
        {
            textWaitTimer = 0;
            go = false;
        }

        //check if the player is facing the interactable object
        IsFacingObject();

        if (TextObject != null)
        {
            if (ready && (Input.GetKey(KeyCode.Space)) && (Player.GetComponent<TankControls>().inputEnabled == true) && (textWaitTimer == 0))
            {
                Component obj = this.gameObject.GetComponent<ChangeRooms>();


                if (obj == null)
                {
                    TextObject.GetComponent<TW_Regular>().StartTypewriter();
                    Player.GetComponent<TankControls>().inputEnabled = false;
                    displayText = true;
                }
                else
                {
                    bool locked = this.gameObject.GetComponent<ChangeRooms>().locked;
                    bool unlocked = this.gameObject.GetComponent<ChangeRooms>().unlocked;
                    bool action = this.gameObject.GetComponent<ChangeRooms>().action;
                    string key = this.gameObject.GetComponent<ChangeRooms>().required_key;

                    if (locked == true || unlocked == true || action == true)
                    {
                        TextObject.GetComponent<TW_Regular>().StartTypewriter();
                        Player.GetComponent<TankControls>().inputEnabled = false;
                        displayText = true;
                    }
                }
            }
        }
    }

    private void IsFacingObject()
    {
        //RAYCASTING
        Vector3 forward = Player.gameObject.transform.TransformDirection(Vector3.forward) * maxDistance;
        RaycastHit hit;
            
        //Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        position0 = new Vector3(Player.transform.position.x, Player.transform.position.y + 0, Player.transform.position.z);
        position1 = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);
        position2 = new Vector3(Player.transform.position.x, Player.transform.position.y + 2, Player.transform.position.z);
        position3 = new Vector3(Player.transform.position.x, Player.transform.position.y + 3, Player.transform.position.z);
        position4 = new Vector3(Player.transform.position.x, Player.transform.position.y + 4, Player.transform.position.z);
        position5 = new Vector3(Player.transform.position.x, Player.transform.position.y + 5, Player.transform.position.z);
        position6 = new Vector3(Player.transform.position.x, Player.transform.position.y + 6, Player.transform.position.z);
        position7 = new Vector3(Player.transform.position.x, Player.transform.position.y + 7, Player.transform.position.z);


        for (int i = 0; i < 7; i++)
        {

            Debug.DrawRay(position0, forward, Color.red);
            Debug.DrawRay(position1, forward, Color.red);
            Debug.DrawRay(position2, forward, Color.red);
            Debug.DrawRay(position3, forward, Color.red);
            Debug.DrawRay(position4, forward, Color.red);
            Debug.DrawRay(position5, forward, Color.red);
            Debug.DrawRay(position6, forward, Color.red);
            Debug.DrawRay(position7, forward, Color.red);
        }

        // check all raycasts, from bottom to top for a collision with this game object



        if ((Physics.Raycast(position0, Player.gameObject.transform.forward, out hit, maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(position1, Player.gameObject.transform.forward, out hit, maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(position2, Player.gameObject.transform.forward, out hit, maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(position3, Player.gameObject.transform.forward, out hit, maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(position4, Player.gameObject.transform.forward, out hit, maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(position5, Player.gameObject.transform.forward, out hit, maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(position6, Player.gameObject.transform.forward, out hit, maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(position7, Player.gameObject.transform.forward, out hit, maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)))
        {
            ready = true;
        }
        else
        {
            ready = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeRooms : MonoBehaviour
{
    public bool ready;
    public bool locked;
    public bool unlocked;
    public bool action;
    public string required_key;
    //public float angle;

    public string level; //what level are you loading?
  
    public GameObject Player;

    //where is the player going to spawn in that level
    public float temp_position_x;
    public float temp_position_y;
    public float temp_position_z;
    public float temp_rotation_x;
    public float temp_rotation_y;
    public float temp_rotation_z;

    private void Update()
    {
        //check if the player is facing the interactable object
        IsFacingObject();

        
        if (ready && (Input.GetKey(KeyCode.Space)) && !locked && !unlocked && !action)
        {

            Component obj = this.gameObject.GetComponent<ChangeRooms>();

            if (obj == null) 
            {
                ChangeScene(level);
            }
            else
            {
                if(DetermineTextObject.textWaitTimer == 0)
                {
                    ChangeScene(level);
                }
            }

        }
        
    }

    //Change Scene By Name
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
        PlayerSpawner.x_point = temp_position_x;
        PlayerSpawner.y_point = temp_position_y;
        PlayerSpawner.z_point = temp_position_z;
        PlayerSpawner.xr_point = temp_rotation_x;
        PlayerSpawner.yr_point = temp_rotation_y;
        PlayerSpawner.zr_point = temp_rotation_z;
    }










    private void IsFacingObject()
    {
        //RAYCASTING
        Vector3 forward = Player.gameObject.transform.TransformDirection(Vector3.forward) * DetermineTextObject.maxDistance;
        RaycastHit hit;

        //Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        DetermineTextObject.position0 = new Vector3(Player.transform.position.x, Player.transform.position.y + 0, Player.transform.position.z);
        DetermineTextObject.position1 = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);
        DetermineTextObject.position2 = new Vector3(Player.transform.position.x, Player.transform.position.y + 2, Player.transform.position.z);
        DetermineTextObject.position3 = new Vector3(Player.transform.position.x, Player.transform.position.y + 3, Player.transform.position.z);
        DetermineTextObject.position4 = new Vector3(Player.transform.position.x, Player.transform.position.y + 4, Player.transform.position.z);
        DetermineTextObject.position5 = new Vector3(Player.transform.position.x, Player.transform.position.y + 5, Player.transform.position.z);
        DetermineTextObject.position6 = new Vector3(Player.transform.position.x, Player.transform.position.y + 6, Player.transform.position.z);
        DetermineTextObject.position7 = new Vector3(Player.transform.position.x, Player.transform.position.y + 7, Player.transform.position.z);


        for (int i = 0; i < 7; i++)
        {

            Debug.DrawRay(DetermineTextObject.position0, forward, Color.red);
            Debug.DrawRay(DetermineTextObject.position1, forward, Color.red);
            Debug.DrawRay(DetermineTextObject.position2, forward, Color.red);
            Debug.DrawRay(DetermineTextObject.position3, forward, Color.red);
            Debug.DrawRay(DetermineTextObject.position4, forward, Color.red);
            Debug.DrawRay(DetermineTextObject.position5, forward, Color.red);
            Debug.DrawRay(DetermineTextObject.position6, forward, Color.red);
            Debug.DrawRay(DetermineTextObject.position7, forward, Color.red);
        }

        // check all raycasts, from bottom to top for a collision with this game object



        if ((Physics.Raycast(DetermineTextObject.position0, Player.gameObject.transform.forward, out hit, DetermineTextObject.maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(DetermineTextObject.position1, Player.gameObject.transform.forward, out hit, DetermineTextObject.maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(DetermineTextObject.position2, Player.gameObject.transform.forward, out hit, DetermineTextObject.maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(DetermineTextObject.position3, Player.gameObject.transform.forward, out hit, DetermineTextObject.maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(DetermineTextObject.position4, Player.gameObject.transform.forward, out hit, DetermineTextObject.maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(DetermineTextObject.position5, Player.gameObject.transform.forward, out hit, DetermineTextObject.maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(DetermineTextObject.position6, Player.gameObject.transform.forward, out hit, DetermineTextObject.maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)) ||
            (Physics.Raycast(DetermineTextObject.position7, Player.gameObject.transform.forward, out hit, DetermineTextObject.maxDistance) && (this.gameObject.name == hit.collider.gameObject.name)))
        {
            ready = true;
        }
        else
        {
            ready = false;
        }
    }
}

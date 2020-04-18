using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static float x_point;
    public static float y_point;
    public static float z_point;
    public static float xr_point = 0;
    public static float yr_point = 0;
    public static float zr_point = 0;
    public GameObject killerSpawner;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (x_point != 0 && y_point != 0 && z_point != 0)
        {
            player.transform.position = new Vector3(x_point, y_point, z_point);
            player.transform.Rotate(xr_point, yr_point, zr_point);
        }

        //always spawn an instance of the "killer spawner" at the start of each scene

        if (!GameObject.Find("KillerSpawner"))
        {
            Vector3 spawnPoint = player.transform.position;
            Quaternion spawnRot = player.transform.rotation;
            Instantiate(killerSpawner, spawnPoint, spawnRot);
        }

        
    }

}

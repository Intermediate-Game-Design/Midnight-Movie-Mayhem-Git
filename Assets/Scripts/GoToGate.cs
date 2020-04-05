using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGate : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other)
    {
        string level = SceneManager.GetActiveScene().name;

        if (other.CompareTag("Player"))
        {
            if (level == "Outdoors_Main")
            {

                SceneManager.LoadScene("Exit_Gate");

                PlayerSpawner.x_point = 0;
                PlayerSpawner.y_point = 0;
                PlayerSpawner.z_point = 0;
                PlayerSpawner.xr_point = 0;
                PlayerSpawner.yr_point = 0;
                PlayerSpawner.zr_point = 0;

            }
            if (level == "Exit_Gate")
            {
                SceneManager.LoadScene("Outdoors_Main");

                float temp_position_x = -41.97404f;
                float temp_position_y = -1.430511e-06f;
                float temp_position_z = 90.71925f;

                float temp_rotation_x = 0;
                float temp_rotation_y = -90f;
                float temp_rotation_z = 0;

                PlayerSpawner.x_point = temp_position_x;
                PlayerSpawner.y_point = temp_position_y;
                PlayerSpawner.z_point = temp_position_z;
                PlayerSpawner.xr_point = temp_rotation_x;
                PlayerSpawner.yr_point = temp_rotation_y;
                PlayerSpawner.zr_point = temp_rotation_z;
            }
        }
    }
}

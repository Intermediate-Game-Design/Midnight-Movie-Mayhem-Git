using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingCameras : MonoBehaviour
{
    public GameObject cameraEnabled; //required
    public GameObject cameraDisabled; //not required, if null

    public bool camOn = false;
    public int cameraNumber;
    private Camera[] cameras;

    void Start()
    {
        cameraNumber = 1;
        GameObject camMaster = GameObject.Find("CameraMaster");
        cameras = camMaster.GetComponent<CameraArray>().cameras;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (cameraDisabled != null)
            {
                cameraDisabled.SetActive(false);
                cameraEnabled.SetActive(true);
            }
            else
            {
                for(int i=0;i<cameras.Length;i++)
                {
                    cameras[i].gameObject.SetActive(false);
                }
                cameraEnabled.SetActive(true);
            }
        }
    }
}

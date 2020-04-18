using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour

{
    public float speed = 2.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed, 0);
    }

    private void Start()
    {
        if(this.gameObject.name == "Pool Ball")
        {

            Debug.Log("pool ball");
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}

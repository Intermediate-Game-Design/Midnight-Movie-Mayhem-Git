using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinter : MonoBehaviour
{
    float stamina = 5, maxStamina = 5;
    float walkSpeed, runSpeed;
    TankControls cm;
    bool isRunning;

    Rect staminaRect;
    Texture2D staminaTexture;

    // Start is called before the first frame update
    void Start()
    {
        cm = gameObject.GetComponent<TankControls>();
        staminaRect = new Rect(Screen.width / 10, Screen.height * 9 /10, 
            Screen.width / 3, Screen.height / 50);
        staminaTexture = new Texture2D(1, 1);
        staminaTexture.SetPixel(0, 0, Color.white);
        staminaTexture.Apply();
    }

    void SetRunning(bool isRunning)
    {
        this.isRunning = isRunning;
        //change maxwalkspeed based on if the isrunning is true or not.
    }

    // Update is called once per frame
    void Update()
    {
        SetRunning(cm.isRunning);


        if (isRunning)
        {
            
            stamina -= Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                
                cm.isRunning = false;
                
                SetRunning(false);
                StartCoroutine(Exhaustion());
                
            }

        }
        else if (stamina < maxStamina)
        {
            stamina += Time.deltaTime;
        }
    }

    void OnGUI()
    {
        float ratio = stamina / maxStamina;
        float rectWidth = ratio*Screen.width / 3;
        staminaRect.width = rectWidth;
        GUI.DrawTexture(staminaRect, staminaTexture);
    }
    IEnumerator Exhaustion()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        cm.canSprint = false;
        yield return new WaitForSeconds(5);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        cm.canSprint = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinter : MonoBehaviour
{
    public float stamina = 5, maxStamina = 5;
    float walkSpeed, runSpeed;
    TankControls cm;
    bool isRunning, hideSprint = true;

    Rect staminaRect;
    Texture2D staminaTexture;

    // Start is called before the first frame update
    void Start()
    {
        cm = gameObject.GetComponent<TankControls>();
        staminaRect = new Rect(20, 20,
                50, 20);
        staminaTexture = new Texture2D(1, 1);
        staminaTexture.SetPixel(0, 0, Color.green);
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            hideSprint = false;
        }
            

        if (stamina > maxStamina)
        {
            StartCoroutine(HideSprint());
            stamina = maxStamina;
        }

                if (isRunning && stamina > 0)
                {
                    stamina -= Time.deltaTime/2;
                }
                if (stamina < 0)
                {
                    stamina = 0;

                    cm.isRunning = false;

                    SetRunning(false);
                    StartCoroutine(Exhaustion());

                }

                else if (stamina < maxStamina && !isRunning)
                {
                    stamina += Time.deltaTime;
                }
            
    }

    void OnGUI()
    {
        float ratio = stamina / maxStamina;
        float rectWidth = ratio*250;
        staminaRect.width = rectWidth;
        if (stamina < maxStamina)
        {
            GUI.DrawTexture(staminaRect, staminaTexture);
        }
        else if (!hideSprint && isRunning){
            GUI.DrawTexture(staminaRect, staminaTexture);
        }
    }
    IEnumerator Exhaustion()
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        cm.canSprint = false;
        yield return new WaitForSeconds(5);
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        cm.canSprint = true;
    }
    IEnumerator HideSprint()
    {
        yield return new WaitForSeconds(.5f);
        hideSprint = true;
    }
}

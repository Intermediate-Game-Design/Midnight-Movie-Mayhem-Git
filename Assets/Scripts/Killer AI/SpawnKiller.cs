using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

public class SpawnKiller : MonoBehaviour
{

    public GameObject killer;
    private GameObject player;
    
    public Vector3 spawnPoint;
    public Quaternion spawnRot;
    private string chasing;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawnPoint = player.transform.position;
        spawnRot = player.transform.rotation;
        chasing = GameState.IniReadValue("Enemy", "Chasing");

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!GameObject.FindWithTag("Killer") && (Vector3.Distance(player.transform.position, spawnPoint) >= 5))
        {
            if (chasing == "true")
            {
                GameState.IniWriteValue("Enemy", "Chasing", "false");
                Instantiate(killer, spawnPoint, spawnRot);
            }
        }
    }
}

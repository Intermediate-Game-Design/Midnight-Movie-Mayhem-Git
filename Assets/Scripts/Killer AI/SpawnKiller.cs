using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnKiller : MonoBehaviour
{

    public GameObject killer;
    private GameObject player;
    private string level;
    public Vector3 spawnPoint;
    public GameObject[] points;
    public Quaternion spawnRot;
    private string chasing;
    public float timer = 0f;
    public int desired_spawn_time;

    // Start is called before the first frame update
    void Start()
    {
        desired_spawn_time = UnityEngine.Random.Range(60, 150); //pick a random time that the killer should spawn in

        level = SceneManager.GetActiveScene().name;
        player = GameObject.FindWithTag("Player");
        chasing = GameState.IniReadValue("Enemy", "Chasing");
        points = GameObject.Find("EnemySpawnPoints").GetComponent<AIPatrol>().spawnPoints;

        if (chasing == "true")
        {
            spawnPoint = player.transform.position;
            spawnRot = player.transform.rotation;
        }
        else
        {
            FindPoint();
        }

    }

    void FindPoint()
    {
        int point = UnityEngine.Random.Range(0, points.Length);
        if (level == "Indoors_Cafe" || level == "Outdoors_Main")
        {
            string door = GameState.IniReadValue("Doors", "Side Door To Inside");
            if (door == "unlocked")
            {
                spawnPoint = points[point].transform.position;
                spawnRot = points[point].transform.rotation;
            }
            else
            {
                FindPoint();
            }
        }
        else
        {
            spawnPoint = points[point].transform.position;
            spawnRot = points[point].transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!GameObject.FindWithTag("Killer") && (Vector3.Distance(player.transform.position, spawnPoint) >= 5))
        {
            if (chasing == "true")
            {
                if (level != "Office" && level != "Shed")
                {
                    GameState.IniWriteValue("Enemy", "Chasing", "false");
                    Instantiate(killer, spawnPoint, spawnRot);
                }
                else
                {
                    GameState.IniWriteValue("Enemy", "Chasing", "false");
                }
            }
            else
            {
                if (level != "Office" && level != "Shed")
                {
                    timer += .1f;
                    if (timer >= desired_spawn_time)
                    {
                        Instantiate(killer, spawnPoint, spawnRot);
                        timer = 0f;
                        desired_spawn_time = UnityEngine.Random.Range(60, 150); //pick a random time that the killer should spawn in
                    }
                }
            }

        }
    }
}

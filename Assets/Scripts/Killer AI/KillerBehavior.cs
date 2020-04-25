using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KillerBehavior : MonoBehaviour
{
    public GameObject Player;
    private NavMeshAgent navMesh;
    public GameObject patrolPointMaster;
    public GameObject[] points;
    public int current_point = 0;

    //perception
    public float maxAngle = 60.5f;
    public float maxRadius;
    public Transform playerPosition;
    public bool isInFOV = false;
    public bool withinRange = false;

    //behavior
    private Vector3 startPosition;
    public Vector3 personalLastSighting;
    public static bool canMove = true; //toggle if inventory is open, or other things
    private bool foundPoint;
    public bool patrolling = true;
    public static bool chasing = false;
    public bool investigating = false;
    private float patrolSpeed = .5f;
    public int patrolRange = 100;
    private GameObject target;

    //draw FOV indicator for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);


        if (!isInFOV)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Vector3 up = new Vector3(transform.position.x, transform.position.y+2, transform.position.z);
        Gizmos.DrawRay(up, (Player.transform.position + (Player.transform.up*4) - transform.position).normalized *maxRadius);
        Gizmos.DrawRay(transform.position, (Player.transform.position + (Player.transform.up*4) - transform.position).normalized *maxRadius);


        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
    }

    //check if AI can see the player
    public static bool inFOV(GameObject checkingObject, GameObject target, float maxAngle, float maxRadius)
    {
        //Debug.Log("Function started");

     Vector3 directionBetween = (target.transform.position + target.transform.up - checkingObject.transform.position).normalized;
     directionBetween.y *= 0;

     float angle = Vector3.Angle(checkingObject.transform.forward, directionBetween);

        if(angle <= maxAngle)
        {

            //Debug.Log("Within angle");

            Vector3 up = new Vector3(checkingObject.transform.position.x, checkingObject.transform.position.y + 2, checkingObject.transform.position.z);
            Ray ray = new Ray(checkingObject.transform.position, target.transform.position + (target.transform.up*4) - checkingObject.transform.position);
            Ray ray2 = new Ray(up, target.transform.position + (target.transform.up*4) - checkingObject.transform.position);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit, maxRadius) || Physics.Raycast(ray2,out hit,maxRadius))
            {
                if (hit.transform.CompareTag("Player"))
                 {

                 //Debug.Log("Player Hit");
                 return true;
                 }
                

            }
        }
        else
        {
            Debug.Log("Not Within angle");
        }
        return false;
    }

    //check for trigger collisions

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            withinRange = true;
            isInFOV = inFOV(this.gameObject, Player, maxAngle, maxRadius);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            withinRange = false;
            isInFOV = false;
        }
    }

    private void Start()
    {
            Player = GameObject.FindWithTag("Player");
            navMesh = this.gameObject.GetComponent<NavMeshAgent>() as NavMeshAgent;
            //animator = GetComponent<Animator>();

            //Killer should start patrolling immediately
            startPosition = this.transform.position; //sets start position before new nav path is created
            patrolPointMaster = GameObject.Find("PatrolPointMaster");
            points = patrolPointMaster.GetComponent<AIPatrol>().patrolPoints;


            Patrol();
        
        maxRadius = 60.5f;

    }
    void Patrol()
    {
        //Debug.Log("Wandering");

        //pick a random point to proceed to
        //Vector3 destination = startPosition + new Vector3(Random.Range(-patrolRange, patrolRange), 0, Random.Range(-patrolRange, patrolRange));

        //pick a point from the patrol points array

        

       

        Vector3 destination = points[current_point].transform.position;
        NewDestination(destination);
    }

    public void NewDestination(Vector3 targetPoint)
    {
        NavMeshPath path = new NavMeshPath();
        navMesh.CalculatePath(targetPoint, path);

        //check if a path to the point is possible
        if (path.status != NavMeshPathStatus.PathPartial)
        {
            navMesh.SetDestination(targetPoint); //make the enemy move toward that point
            foundPoint = true; //used in coroutine to only run the function once
        }
        //else
        //{
        //    Patrol(); //try for a new point that works
        //}
        //if (current_point != points.Length-1)
        //{ 
        //}
    }

    private IEnumerator DelayPatrol()
    {
        yield return new WaitForSeconds(3f); //wait 3 seconds before setting a new point to move to

        if (!foundPoint) //has a point already been decided?
        {
            if (investigating)
            {
                investigating = false;
                patrolling = true;
            }
            else
            {
                current_point++;
            }
            Patrol();
        }
    }

    private void Update()
    {
        if (isInFOV)
        {
            chasing = true;
            patrolling = false;
            investigating = false;
        }
        else if (chasing)
        {
            chasing = false;
            investigating = true;
            personalLastSighting = Player.transform.position;
        }
        else if (!investigating)
        {
            patrolling = true;
            chasing = false;
            investigating = false;
        }


        if (patrolling)
        {
            //determine if killer has reached destination

            float dist = navMesh.remainingDistance;
            if (dist <= navMesh.stoppingDistance)
            {
                if (navMesh.hasPath || navMesh.velocity.sqrMagnitude == 0f)
                {
                    if (current_point != points.Length-1)
                    {
                        StartCoroutine(DelayPatrol()); //wait a few seconds before finding a new destination
                        foundPoint = false; //reset bool
                    }
                    else if (Vector3.Distance(transform.position,points[points.Length-1].transform.position) <= 5)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }

        //lost sight of player
        if (investigating)
        {
            Vector3 point = personalLastSighting;
            navMesh.SetDestination(point);


            float dist = navMesh.remainingDistance;
            if (dist <= navMesh.stoppingDistance)
            {
                if (navMesh.hasPath || navMesh.velocity.sqrMagnitude == 0f)
                {
                    navMesh.velocity = Vector3.zero;
                    StartCoroutine(DelayPatrol()); //wait a few seconds before finding a new destination
                    foundPoint = false; //reset bool
                }
            }
        }


        //player in sight, start chasing
        if (chasing)
        {
            Vector3 point = Player.transform.position;
            navMesh.SetDestination(point);
            float distance = Vector3.Distance(point, transform.position);


            //stop enemy when they are close enough to the player
            if (distance <= 5)
            {
                navMesh.velocity = Vector3.zero;
                navMesh.isStopped = true;
            }
            else
            {
                navMesh.isStopped = false;
            }
        }


        //freeze the killer if they aren't allowed to move at any point
        if (!canMove)
        {
            navMesh.velocity = Vector3.zero;
        }
      
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemy : MonoBehaviour
{
    // Custom Variables
    [SerializeField] protected AlienData myData;
    [SerializeField] private GameObject visionRay;
    private GameObject player;
    private enum EnemyBehaviour {Look, Chase, Patrol}
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    private Vector3 distance = new Vector3(0, 0, 0);
    private int waypointIndex;
    private float dist;
    [SerializeField] Transform[] waypoints;
    private Rigidbody rbTentacle;


    void Start()
    {
        player = GameObject.Find("Player");
        rbTentacle = GetComponent<Rigidbody>();
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
       /* Vector3 direction = waypoints[waypointIndex].position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        rbTentacle.MoveRotation(newRotation);*/
    }


    void Update()
    {
        SetEnemyBehaviour(enemyBehaviour);
        //Raycast();
    }

   /* void FixedUpdate()
    {
        SetEnemyBehaviour(enemyBehaviour);
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 4f)
        {
            IncreaseIndex();
        }

    }*/

    void Patrol()
    {
       /* rbTentacle.MovePosition(rbTentacle.position + transform.forward * myData.speedEnemy * Time.deltaTime);
       Vector3 direction = waypoints[waypointIndex].position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        rbTentacle.MoveRotation(newRotation);*/
       transform.Translate(Vector3.forward * myData.speedEnemy * Time.deltaTime);
        transform.LookAt(waypoints[waypointIndex].position);
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 3f)
        {
            IncreaseIndex();
        }
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
       /* Vector3 direction = waypoints[waypointIndex].position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        rbTentacle.MoveRotation(newRotation);*/
    }

    private void MoveEnemy(Vector3 direction)
    {
      transform.Translate(myData.speedEnemy * Time.deltaTime * direction);
    }

    private void MoveTowards()
    {
        Vector3 direction = ((player.transform.position + distance) - transform.position).normalized;
        transform.position += myData.speedEnemy * direction * Time.deltaTime; 
    }

    private void LookAtPlayer ()
    {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, myData.speedToLook * Time.deltaTime);
    }

    private void SetEnemyBehaviour(EnemyBehaviour enemyBehaviour)
    {
        switch (enemyBehaviour)
        {
            case EnemyBehaviour.Look:
                LookAtPlayer();
                break;

            case EnemyBehaviour.Chase:
                LookAtPlayer();
                MoveTowards();
                break;

            case EnemyBehaviour.Patrol:
                Patrol();
                break;

        }
    }

    private void Raycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(visionRay.transform.position, visionRay.transform.TransformDirection(Vector3.forward), out hit, myData.distanceRay))
        {
            if(hit.transform.tag == "Player")
            {
                enemyBehaviour = EnemyBehaviour.Chase;
                Debug.Log("El Alien te vio");
            }            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay (visionRay.transform.position, visionRay.transform.TransformDirection(Vector3.forward) * myData.distanceRay);
    }
}

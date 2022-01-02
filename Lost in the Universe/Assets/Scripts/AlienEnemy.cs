using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemy : MonoBehaviour
{
    // Custom Variables
    [SerializeField] protected AlienData myData;
    [SerializeField] private GameObject visionRay;
    private GameObject player;
    private enum EnemyBehaviour {Patrol, Attack}
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    private Vector3 distance = new Vector3(0, 0, 0);
    private int waypointIndex;
    private float dist;
    private float distPlayer;
    [SerializeField] Transform[] waypoints;
    private Rigidbody rbTentacle;
    private GameObject level2;
    private int level = 1;
    private Animator anim;
    private bool attack = false;
    [SerializeField] GameObject bulletPrefab;
    private bool canShoot = true;
    [SerializeField] private float shootCoolDown = 0.8f;
    [SerializeField] private float timeShoot = 2f;

    void Start()
    {
        player = GameObject.Find("Player");
        rbTentacle = GetComponent<Rigidbody>();
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        anim.SetBool("attack", attack);
        SetEnemyBehaviour(enemyBehaviour);
        FindLevel();
        timeShoot += Time.deltaTime;
        if (level == 2 )
        {
        Raycast();
        DistancePlayer();
        }
    }

    private void FindLevel()
    {
        level2 = GameObject.Find("level2");
        if ( level2 != null ) 
        {
            level = 2;
        }
    }

    void DistancePlayer()
    {
        distPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distPlayer > 20f)
        {
            enemyBehaviour = EnemyBehaviour.Patrol;
        }
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * myData.speedEnemy * Time.deltaTime);
        transform.LookAt(waypoints[waypointIndex].position);
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 3f)
        {
            IncreaseIndex();
        }
        attack = false;
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
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

    private void Attack ()
    {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, myData.speedToLook * Time.deltaTime);
        attack = true;
        if(timeShoot > shootCoolDown)
        {
        GameObject b = Instantiate(bulletPrefab, visionRay.transform.position, bulletPrefab.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce (visionRay.transform.TransformDirection(Vector3.forward) * 20f , ForceMode.Impulse);
        AudioManager.instance.AlienSpitSFX();
        timeShoot = 0;
        }
    }

    private void SetEnemyBehaviour(EnemyBehaviour enemyBehaviour)
    {
        switch (enemyBehaviour)
        {
            case EnemyBehaviour.Patrol:
                Patrol();
                break;

            case EnemyBehaviour.Attack:
                Attack();
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
                enemyBehaviour = EnemyBehaviour.Attack;
            }    
        }
    }

    private void OnDrawGizmos()
    {   
        Gizmos.color = Color.blue;
        Gizmos.DrawRay (visionRay.transform.position, visionRay.transform.TransformDirection(Vector3.forward) * myData.distanceRay);
    }
}

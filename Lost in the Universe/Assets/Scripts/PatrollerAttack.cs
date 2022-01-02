using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerAttack : Patroller
{

    [SerializeField] private GameObject visionRay;
    private enum EnemyBehaviour {Patrol, Attack};
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    private float speedToLook = 5f;
    private float distanceRay = 8f;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetEnemyBehaviour(enemyBehaviour);
        Raycast();
    }

    private void LookAtPlayer ()
    {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime);
    }


    private void SetEnemyBehaviour(EnemyBehaviour enemyBehaviour)
    {
        switch (enemyBehaviour)
        {
            case EnemyBehaviour.Patrol:
                Patrol();
                break;

            case EnemyBehaviour.Attack:
                LookAtPlayer();
                break;
        }
    }
    
    private void Raycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(visionRay.transform.position, visionRay.transform.TransformDirection(Vector3.forward), out hit, distanceRay))
        {
            if(hit.transform.tag == "Player")
            {
                enemyBehaviour = EnemyBehaviour.Attack;
                Debug.Log("El Alien te vio");
            }            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay (visionRay.transform.position, visionRay.transform.TransformDirection(Vector3.forward) * distanceRay);
    }


}

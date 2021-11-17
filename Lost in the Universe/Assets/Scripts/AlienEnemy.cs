using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemy : MonoBehaviour
{
    // Custom Variables

    [SerializeField] private GameObject visionRay;
    [SerializeField] private float distanceRay = 10.0f;
    [SerializeField] private float speedEnemy = 3f;
    [SerializeField] private float speedToLook = 5f;
    private GameObject player;
    private enum EnemyBehaviour {Look, Chase}
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    private Vector3 distance = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SetEnemyBehaviour(enemyBehaviour);
        Raycast();
    }

    private void MoveEnemy(Vector3 direction)
    {
      transform.Translate(speedEnemy * Time.deltaTime * direction);
    }

    private void MoveTowards()
    {
        Vector3 direction = ((player.transform.position + distance) - transform.position).normalized;
        transform.position += speedEnemy * direction * Time.deltaTime; 
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
            case EnemyBehaviour.Look:
                LookAtPlayer();
                break;

            case EnemyBehaviour.Chase:
                LookAtPlayer();
                MoveTowards();
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
                enemyBehaviour = EnemyBehaviour.Chase;
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

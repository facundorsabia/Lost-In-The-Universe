using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAlienEnemy : MonoBehaviour
{
    // Custom Variables
    [SerializeField] protected RedAlienData myData;
    private Animator animRedAlien;
	private CharacterController controller;
    private bool battle = false;
    private bool canShoot = true;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private GameObject visionRay;
    private GameObject player;
    private enum EnemyBehaviour {Look, Chase}
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    private Vector3 distance = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        animRedAlien = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        animRedAlien.SetBool("battle", battle);
        if (canShoot)
        {
            Raycast();
        }else
        {
            myData.timeShoot += Time.deltaTime;
            battle = false;
        }
        if (myData.timeShoot > myData.shootCoolDown)
        {
            canShoot = true;
        }
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


    private void Raycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(visionRay.transform.position, visionRay.transform.TransformDirection(Vector3.forward), out hit, myData.distanceRay))
        {
            if(hit.transform.tag == "Player")
            {
                canShoot = false;
                myData.timeShoot = 0;
                battle = true;
                GameObject b = Instantiate(bulletPrefab, visionRay.transform.position, bulletPrefab.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce (visionRay.transform.TransformDirection(Vector3.forward) * 10f , ForceMode.Impulse);
            }            
        }
    }

    private void OnDrawGizmos()
    {
        if(canShoot)
        {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay (visionRay.transform.position, visionRay.transform.TransformDirection(Vector3.forward) * myData.distanceRay);
        }
    }
}

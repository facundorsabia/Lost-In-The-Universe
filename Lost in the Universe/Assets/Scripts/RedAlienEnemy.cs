using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAlienEnemy : MonoBehaviour
{
    // Custom Variables
    [SerializeField] protected RedAlienData myData;
    private Animator animRedAlien;
	private CharacterController controller;
    protected bool battle = false;
    protected bool canShoot = true;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private GameObject visionRay;
    private GameObject player;
    private enum EnemyBehaviour {Look, Chase}
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    private Vector3 distance = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    public virtual void Start()
    {
        animRedAlien = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    public virtual void  Update()
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


    private void LookAtPlayer ()
    {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, myData.speedToLook * Time.deltaTime);
    }


    protected void Raycast()
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
                b.GetComponent<Rigidbody>().AddForce (visionRay.transform.TransformDirection(Vector3.forward) * 20f , ForceMode.Impulse);
                AudioManager.instance.AlienSpitSFX();
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

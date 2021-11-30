using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rbAlien;
    private Animator animAlien;
    [SerializeField] private float speedEnemy = 3f;
    [SerializeField] private float distanceRay = 10f;
    [SerializeField] private GameObject originOne;
    private bool isAttack = false;


    // Start is called before the first frame update
    void Start()
    {
        rbAlien = GetComponent<Rigidbody>();
        animAlien = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(!isAttack)
        {
            FindEnemy();
            Move();
        }
    }

    public void Move()
    {
        Vector3 direction = Vector3.left;
       rbAlien.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
       rbAlien.AddForce(direction * speedEnemy, ForceMode.Impulse);
    }

    public void FindEnemy()
    {
        BroadcastRaycast(originOne.transform);
    }

    private void BroadcastRaycast(Transform origen)
    {
        RaycastHit hit;
        if(Physics.Raycast(origen.position, origen.TransformDirection(Vector3.forward), out hit, distanceRay))
        {
            if(hit.transform.CompareTag("Player"))
            {
                isAttack = true;
                rbAlien.velocity = Vector3.zero;
                animAlien.SetBool("isAttack", isAttack);
            }
        }
    }

    public void DrawRay(Transform origen)
    {
        Gizmos.color = Color.blue;
        Vector3 direction = origen.TransformDirection(Vector3.forward) * distanceRay;
        Gizmos.DrawRay(origen.position, direction);
    }

    private void OnDrawGizmos()
    {
        DrawRay(originOne.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

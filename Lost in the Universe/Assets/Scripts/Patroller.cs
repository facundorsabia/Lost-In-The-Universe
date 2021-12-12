using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed;
    //[SerializeField] private float gravity = -9.8f;
    private int waypointIndex;
    private float dist;
    private Rigidbody rbTurtle;
    //private Vector3 moveDirection;
    //private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rbTurtle = GetComponent<Rigidbody>();
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        //velocity.y += gravity * Time.deltaTime;
        //moveDirection = Vector3.forward;
    }

    void FixedUpdate() 
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 3f)
        {
            IncreaseIndex();
        }
        Patrol();
        transform.LookAt(waypoints[waypointIndex].position);
    }


    void Patrol()
    {
        //rbTurtle.AddForce(Vector3.forward * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //rbTurtle.MovePosition(rbTurtle.position + transform.forward * speed * Time.deltaTime);
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
}

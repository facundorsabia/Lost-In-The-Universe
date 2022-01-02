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
    protected GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rbTurtle = GetComponent<Rigidbody>();
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
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


    protected void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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

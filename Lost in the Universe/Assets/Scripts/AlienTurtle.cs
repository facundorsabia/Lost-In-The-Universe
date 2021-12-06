using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTurtle : RedAlienEnemy
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float minimumDistance;
    private int currentIndex = 0;

     public override void Update()
    {
        base.Update();
        //Patrol();
    }


    private void Patrol()
    {
        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        Vector3 direction = deltaVector.normalized;
        transform.position += direction * myData.speedEnemy * Time.deltaTime;
        float distance = deltaVector.magnitude;
        if(distance < minimumDistance)
        {
            currentIndex ++;
        }
    }
}

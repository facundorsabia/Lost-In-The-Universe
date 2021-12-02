using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RedAlienData", menuName = "Red Alien Data")]

public class RedAlienData : ScriptableObject
{
   // Custom Variables
    public float shootCoolDown = 1.95f;
    public float timeShoot = 2f;
    public float distanceRay = 10.0f;
    public float speedEnemy = 3f;
    public float speedToLook = 5f;
}

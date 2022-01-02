using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AlienData", menuName = "Alien Data")]

public class AlienData : ScriptableObject
{
    public string alienName = "Tentacle";
    public float hP = 10.0f;
    public float distanceRay = 10.0f;
    public float speedEnemy = 3f;
    public float speedToLook = 5f;
}

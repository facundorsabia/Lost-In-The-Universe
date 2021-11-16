using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    // Custom Variables
    [SerializeField] private float speedEnemy = 3f;
    [SerializeField] private float speedToLook = 5f;
    private GameObject player;
    private Vector3 distance = new Vector3(2, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        MoveTowards();
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

}

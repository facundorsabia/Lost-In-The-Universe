using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float timeBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeBullet -= Time.deltaTime;
        if(timeBullet < 0 )
        {
         Destroy(gameObject);
        }
    }

}

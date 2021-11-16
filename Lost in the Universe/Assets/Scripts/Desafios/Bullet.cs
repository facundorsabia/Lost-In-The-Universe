using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 1f;

    [SerializeField] private float timeBullet = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeBullet -= Time.deltaTime;
        if(timeBullet > 0 ){
            Shoot(Vector3.right);
        }else
        {
            Destroy(gameObject);
        }
        
    }

    private void Shoot(Vector3 direction) {
        transform.Translate(direction * Time.deltaTime * speed);
    }
}

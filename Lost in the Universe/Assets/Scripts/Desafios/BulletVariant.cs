using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVariant : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direction = new Vector3 (0, 0, 1f);
    public float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot() {
        transform.Translate(direction * Time.deltaTime * speed);
    }
}

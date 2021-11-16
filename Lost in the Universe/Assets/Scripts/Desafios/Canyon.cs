using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canyon : MonoBehaviour
{
    public GameObject[] bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 1f, 1.5f);      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Shoot()
    {
        int bulletIndex = Random.Range (0, bulletPrefab.Length);
        Instantiate(bulletPrefab[bulletIndex], transform.position, bulletPrefab[bulletIndex].transform.rotation);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPosition = new Vector3 (-11,3,9);
    public GameObject bulletPrefab;

    public float instantiateTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 1f, instantiateTime);      
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, spawnPosition, bulletPrefab.transform.rotation);
    }

    private void Bulletx2(){
            if(Input.GetKeyDown(KeyCode.Space))
        {
            bulletPrefab.transform.localScale = bulletPrefab.transform.localScale * 2;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
        Debug.Log("Has conseguido combustible para tu nave!");
        Destroy(gameObject);
        }
    }
}
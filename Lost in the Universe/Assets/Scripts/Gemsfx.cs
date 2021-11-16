using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemsfx : MonoBehaviour
{
    AudioSource gemSfx;
    // Start is called before the first frame update
    void Start()
    {
        gemSfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other)
    {
        gemSfx.Play();
        if (other.gameObject.layer == 7)
        {
        Debug.Log("sound");
        gemSfx.Play();
        }   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    private int win = 0;

    // Start is called before the first frame update
    void Start()
    {
       PlayerAstronaut.onWinLevel+= OnWinHandler;
    }

    // Update is called once per frame
    void Update()
    {
        TakeOff();
    }

    private void OnWinHandler()
    {
        Debug.Log("despegando");
        win = 10;
    }

    private void TakeOff()
    {
        if (win == 10)
        { 
        transform.Translate(Vector3.up * 5f * Time.deltaTime);
        transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }
    }
}

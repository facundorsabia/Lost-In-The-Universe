using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField] private List <GameObject> cameras;
    [SerializeField] private int indexCamera = 0;
    private int win = 0;

    void Awake()
    {
        indexCamera = 0;
        SwitchCamera(indexCamera);
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerAstronaut.onWinLevel += OnWinHandler;
        PlayerAstronaut.onDeath += OnDeadHandler;
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.F1))
         {
             indexCamera++;
             if(indexCamera == cameras.Count)
             {
                 indexCamera = 0;
             }
             SwitchCamera(indexCamera);
         }
        if (win == 10)
        {
            indexCamera = 2;
            SwitchCamera(indexCamera);
        }
        if (win == 5)
        {
            indexCamera = 3;
            SwitchCamera(indexCamera);
            cameras[3].transform.position += new Vector3(0, 1, 0)* 3f * Time.deltaTime;
        }
    }

    void SwitchCamera (int index)
    {
        foreach (var camera in cameras)
        {
            camera.SetActive(false);
        }
        cameras[index].SetActive(true);
    }

    private void OnWinHandler()
    {
        win = 10;
    }

    private void OnDeadHandler()
    {
        win = 5;
    }
}

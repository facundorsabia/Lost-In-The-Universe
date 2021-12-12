using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField] private List <GameObject> cameras;
    [SerializeField] private int indexCamera = 0;
    private int win = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAstronaut.onWinLevel += OnWinHandler;
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
}

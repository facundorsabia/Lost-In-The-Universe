using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameover : MonoBehaviour
{
    
    public static gameover instance;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject instructionOne;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerAstronaut.onDeath += OnDeadHandler;
        gameOver.SetActive(false);
        instructionOne.SetActive(false);
        Invoke("FirstInstruction", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void FirstInstruction()
    {
        instructionOne.SetActive(true);
        Invoke("HideFirstInstruction", 3f);
    }

    private void HideFirstInstruction()
    {
        instructionOne.SetActive(false);
    }

    private void OnDeadHandler()
    {
        gameOver.SetActive(true);
      Invoke("HideGameOVerScreen", 4f);
       Invoke("FirstInstruction", 5f);
    }

    private void HideGameOVerScreen()
    {
        gameOver.SetActive(false);
    }

}

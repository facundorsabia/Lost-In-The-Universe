using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameover : MonoBehaviour
{
    
    public static gameover instance;
    [SerializeField] private GameObject gameOver;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnDeadHandler()
    {
    gameOver.SetActive(true);
      Invoke("HideGameOVerScreen", 4f);
    }

    private void HideGameOVerScreen()
    {
        gameOver.SetActive(false);
    }

}

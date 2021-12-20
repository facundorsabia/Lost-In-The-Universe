using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Gem Types
    public enum typesGem {Gem, SuperGem, HiperGem};

    private int score;
    private int playerLives;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            score = 0;
            playerLives = 2;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void addScore()
    {
        instance.score += 1;
    }
    public static int getScore()
    {
        return instance.score;
    }

    public static void HealPlayer()
    {
        instance.playerLives += 1;
    }

   public static void DamagePlayer()
    {
        instance.playerLives -= 1;
    }
    public static int GetPlayerLives()
    {
        return instance.playerLives;
    }

    private void OnDeadHandler()
    {
        Invoke("Restart", 2f);
    }

    private void Restart()
    {
         SceneManager.LoadScene("Landing");
    }
}

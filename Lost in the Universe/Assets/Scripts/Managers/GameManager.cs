using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject instructionTwo;
    [SerializeField] private GameObject instructionFinalGem;
    [SerializeField] private GameObject instructionComeBacktoShip;
    private int firstGem = 1;
    private int finalGem = 1;
    private int comeBackGem= 1;

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
            playerLives = 7;
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
        instructionTwo.SetActive(false);
        instructionFinalGem.SetActive(false);
        instructionComeBacktoShip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SecondInstruction();
        FinalGemInstruction();
        ComeBackToShipInstruction();
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

    private void SecondInstruction()
    {
        if (score == 1 && firstGem == 1)
        {
            instructionTwo.SetActive(true);
            Invoke("HideSecondInstruction", 3f);
        }
    }

    private void HideSecondInstruction()
    {
        instructionTwo.SetActive(false);
        firstGem = 2;
    }

    private void FinalGemInstruction()
    {
        if (score == 14 && finalGem == 1)
        {
            instructionFinalGem.SetActive(true);
            Invoke("HideFinalGemInstruction", 3f);
        }
    }

    private void HideFinalGemInstruction()
    {
        instructionFinalGem.SetActive(false);
        finalGem = 2;
    }

        private void ComeBackToShipInstruction()
    {
        if (score == 15 && finalGem == 1)
        {
            instructionComeBacktoShip.SetActive(true);
            Invoke("HideComeBackToShipInstruction", 3f);
        }
    }

    private void HideComeBackToShipInstruction()
    {
        instructionComeBacktoShip.SetActive(false);
        finalGem = 2;
    }

    private void OnDeadHandler()
    {
        playerLives = 7;
        score = 0;
        firstGem = 1;
        finalGem = 1;
        Debug.Log("Game Over");
        Invoke("Restart", 4f);
    }

    private void Restart()
    {

        SceneManager.LoadScene("LostInTheUniverse");
    }
}

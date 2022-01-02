using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject instructionOne;
    [SerializeField] private Text instructionOneText;
    [SerializeField] private GameObject instructionTwo;
    [SerializeField] private GameObject instructionFinalGem;
    [SerializeField] private GameObject instructionComeBacktoShip;
    [SerializeField] private GameObject instructionJumpWater;
    private int firstGem = 1;
    private int finalGem = 1;
    private int comeBackGem= 1;
    private int level = 1;
    private GameObject level2;
    private int firstInstructionController = 1;

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
        PlayerAstronaut.onWinLevel += OnWinHandler;
        instructionOne.SetActive(false);
        Invoke("FirstInstruction", 1f);
        instructionTwo.SetActive(false);
        instructionFinalGem.SetActive(false);
        instructionComeBacktoShip.SetActive(false);
        instructionJumpWater.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SecondInstruction();
        FinalGemInstruction();
        ComeBackToShipInstruction();
        FindLevel();
    }

    public void FindLevel()
    {
        level2 = GameObject.Find("level2");
        if ( level2 != null ) 
        {
            if(firstInstructionController == 1)
            {
            Invoke("FirstInstruction", 1f);
            }
            level = 2;
        }
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


    private void FirstInstruction()
    {
        instructionOne.SetActive(true);
        if(level==2)
        {
        instructionOneText.text="Nuevo planeta comandante, encuentra al menos 40 diamantes.";
        Invoke("JumpWaterInstruction", 4f);
        firstInstructionController = 2;
        }
        Invoke("HideFirstInstruction", 3f);
    }

    private void HideFirstInstruction()
    {
        instructionOne.SetActive(false);
    }

    private void JumpWaterInstruction()
    {
        instructionJumpWater.SetActive(true);
        Invoke("HideJumpWaterInstruction", 5f);
    }

    private void HideJumpWaterInstruction()
    {
        instructionJumpWater.SetActive(false);
    }

    private void SecondInstruction()
    {
        if (score == 1 && firstGem == 1 && level ==1)
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
        if (score == 14 && finalGem == 1 && level ==1)
        {
            instructionFinalGem.SetActive(true);
            Invoke("HideFinalGemInstruction", 3f);
        }
        if (score == 39 && finalGem == 1 && level ==2)
        {
            instructionFinalGem.SetActive(true);
            Invoke("HideFinalGemInstruction", 3f);
        }
    }

    private void HideFinalGemInstruction()
    {
        instructionFinalGem.SetActive(false);
        finalGem = 3;
    }

    private void ComeBackToShipInstruction()
    {
        if (score == 40 && finalGem == 3 && level==1)
        {
            instructionComeBacktoShip.SetActive(true);
            Invoke("HideComeBackToShipInstruction", 3f);
        }
        if (score == 3 && finalGem == 3 && level == 2)
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
        Invoke("FirstInstruction", 5f);
    }

    private void Restart()
    {
        if(level == 1)
        {
            SceneManager.LoadScene("LostInTheUniverse");
        }
        if (level == 2)
        {
            SceneManager.LoadScene("LostInTheUniverseLevel2");
        }
    }

    private void OnWinHandler()
    {
        playerLives = 7;
        finalGem = 3;
    }
}

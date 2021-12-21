using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{

    [SerializeField] private Text textGem;
    [SerializeField] private Text textLives;
    [SerializeField] private Text score;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private InventoryManager mgInventory;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerAstronaut.onDeath += OnDeadHandler;
        PlayerAstronaut.onWinLevel += OnWinHandler;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGemUI();
        UpdateLivesUI();
    }

    void UpdateGemUI()
    {
        int[] gemCount = mgInventory.GetGemQuantity();
        textGem.text = "" + gemCount[0];
    }

    void UpdateLivesUI()
    {
        int playerLives = GameManager.GetPlayerLives();
        textLives.text = "" + playerLives;
    }

    private void OnDeadHandler()
    {
        gameOver.SetActive(true);
        textLives.text = "0";
        int[] gemCount = mgInventory.GetGemQuantity();
        score.text = "" + gemCount[0] + " POINTS";
    }

    private void OnWinHandler()
    {
        textLives.text = "0";
    }

    public void RestartButton(){
        SceneManager.LoadScene("LostInTheUniverse");
    }

    public void ExitButton(){
        SceneManager.LoadScene("Landing");
    }
}

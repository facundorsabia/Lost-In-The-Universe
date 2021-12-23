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
        PlayerAstronaut.onDeath += OnDeadHandler;
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
        if(mgInventory!=null)
        {
        int[] gemCount = mgInventory.GetGemQuantity();
        textGem.text = "" + gemCount[0];
        }else
        {
        textGem.text = "0";
        }
    }

    void UpdateLivesUI()
    {
        int playerLives = GameManager.GetPlayerLives();
        textLives.text = "" + playerLives;
    }

    private void OnDeadHandler()
    {

    }


    private void OnWinHandler()
    {
      // score.text = "Has conseguido ganar";
    }

    public void RestartButton(){
        SceneManager.LoadScene("LostInTheUniverse");
    }

    public void ExitButton(){
        SceneManager.LoadScene("Landing");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{

    [SerializeField] private Text textGem;
    [SerializeField] private Text textLives;
    [SerializeField] private TextMeshProUGUI gameOver;
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
        gameOver.text = "GAME OVER";
        textLives.text = "0";
    }

    private void OnWinHandler()
    {
        gameOver.text = "";
        textLives.text = "0";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{

    [SerializeField] private Text textGem;
    [SerializeField] private GameObject textGemPosition;
    [SerializeField] private Text textLives;
    [SerializeField] private Text score;
    [SerializeField] private GameObject winLevel;
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private InventoryManager mgInventory;
    private GameObject level2;
    private int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAstronaut.onDeath += OnDeadHandler;
        PlayerAstronaut.onWinLevel += OnWinHandler;
        winLevel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGemUI();
        UpdateLivesUI();
        FindLevel();
    }

    void UpdateGemUI()
    {
        textGemPosition.GetComponent<RectTransform>().anchoredPosition = new Vector2(-355f, -14f);
        if(mgInventory!=null)
        {
        int[] gemCount = mgInventory.GetGemQuantity();
        textGem.text = gemCount[0].ToString();
        if(gemCount[0] >= 10)
        {
            textGemPosition.GetComponent<RectTransform>().anchoredPosition = new Vector2(-360f, -14f);
        }
        }else
        {
        textGem.text = "0";
        textGemPosition.GetComponent<RectTransform>().anchoredPosition = new Vector2(-355f, -14f);
        }
    }

    void UpdateLivesUI()
    {
        int playerLives = GameManager.GetPlayerLives();
        textLives.text = "" + playerLives;
    }

    public void FindLevel()
    {
        level2 = GameObject.Find("level2");
        if ( level2 != null ) 
        {
            level = 2;
        }
    }


    private void OnDeadHandler()
    {

    }

    private void OnWinHandler()
    {
        winLevel.SetActive(true);
        score.text = "Lo has conseguido Comandante, sigamos viaje!";
        if(level==2)
        {
            nextLevel.SetActive(false);
            quitButton.SetActive(true);
        }
    }

    public void NextLevelButton(){
        SceneManager.LoadScene("LostInTheUniverseLevel2");
    }

    public void ExitButton(){
    Application.Quit();
    Debug.Log("salir");
    }

    void OnDestroy()
    {
        PlayerAstronaut.onDeath -= OnDeadHandler;
        PlayerAstronaut.onWinLevel -= OnWinHandler;
    }
}

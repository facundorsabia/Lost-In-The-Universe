using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;
    [SerializeField] private Text textGem;
    [SerializeField] private Text textLives;
    [SerializeField] private Text score;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private InventoryManager mgInventory;

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
        PlayerAstronaut.onWinLevel += OnWinHandler;
        gameOver.SetActive(false);
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
        gameOver.SetActive(true);
        
        //textLives.text = "0";
        /*if(mgInventory!=null)
        {
        score.text = "SCORE";
        int[] gemCount = mgInventory.GetGemQuantity();
        score.text = "" + gemCount[0] + " POINTS";
        }*/
       Invoke("HideGameOVerScreen", 2f);
    }

    private void HideGameOVerScreen()
    {
        gameOver.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text textGem;
    [SerializeField] private Text textLives;

    [SerializeField] private InventoryManager mgInventory;
    // Start is called before the first frame update
    void Start()
    {

        textLives.text = "7";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGemUI();
    }

    void UpdateGemUI()
    {
        int[] gemCount = mgInventory.GetGemQuantity();
        textGem.text = "" + gemCount[0];
    }
}

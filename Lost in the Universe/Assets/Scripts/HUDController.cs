using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text textGem;
    [SerializeField] private Text textLives;

    // Start is called before the first frame update
    void Start()
    {
        textGem.text = "A";
        textLives.text = "B";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

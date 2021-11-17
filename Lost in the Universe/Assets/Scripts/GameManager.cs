using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int score;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            score = 0;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcess : MonoBehaviour
{
    private PostProcessVolume globalVolume;
    private int gameOver = 0;

    void Awake()
    {
        globalVolume = GetComponent<PostProcessVolume>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerAstronaut.onDamage += statusColorEffect;
        PlayerAstronaut.onDeath += OnDeadHandler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDeadHandler()
    {
        gameOver = 10;
    }

    public void statusColorEffect(bool status)
    {
        if(gameOver!= 10)
        {
        ColorGrading colorFX;
        globalVolume.profile.TryGetSettings(out colorFX);
        colorFX.active = status;
        }
    }
}

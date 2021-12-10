using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcess : MonoBehaviour
{
    private PostProcessVolume globalVolume;

    void Awake()
    {
        globalVolume = GetComponent<PostProcessVolume>();
    }

    // Start is called before the first frame update
    void Start()
    {
       PlayerAstronaut.onDamage += statusColorEffect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void statusColorEffect(bool status)
    {
        ColorGrading colorFX;
        globalVolume.profile.TryGetSettings(out colorFX);
        colorFX.active = status;
    }

}

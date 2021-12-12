using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource gemSource;
    [SerializeField] List<AudioClip> gemClips = new List<AudioClip>();
    [SerializeField] AudioSource damageSource;
    [SerializeField] List<AudioClip> damageClips = new List<AudioClip>();
    [SerializeField] AudioSource alienAttackSource;
    [SerializeField] List<AudioClip> alienAttackClips = new List<AudioClip>();

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GemSFX()
    {
        AudioClip clip = gemClips[0];
        gemSource.PlayOneShot(clip, 0.1f);
    }

    public void MushroomSFX()
    {
        AudioClip clip = gemClips[1];
        gemSource.PlayOneShot(clip, 0.9f);
    }

    public void DamageSFX()
    {
        AudioClip clip = damageClips[0];
        damageSource.PlayOneShot(clip, 0.5f);
    }

    public void AlienSpitSFX()
    {
        AudioClip clip = alienAttackClips[0];
        alienAttackSource.PlayOneShot(clip, 0.2f);
    }
}

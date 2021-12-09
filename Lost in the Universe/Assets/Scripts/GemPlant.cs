using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GemPlant : MonoBehaviour
{
    [SerializeField] private UnityEvent onShowGems;
    [SerializeField] private ParticleSystem shootParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onShowGems?.Invoke();
            shootParticle.Play();
            Debug.Log("Unity Event");
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}

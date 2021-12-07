using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GemPlant : MonoBehaviour
{
    [SerializeField] private UnityEvent onShowGems;

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
            Debug.Log("Unity Event");
            gameObject.SetActive(false);
        }
    }
}

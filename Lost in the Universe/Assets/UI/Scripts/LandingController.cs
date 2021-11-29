using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LandingController : MonoBehaviour
{
    [SerializeField] private InputField inputUserName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeInputUsername()
    {
        Debug.Log("change");
        Debug.Log(inputUserName.text);
    }

    public void OnEndEditInputUsername()
    {
        Debug.Log("End Edit");
        ProfileManager.instance.SetPlayerName(inputUserName.text);
        Debug.Log("Username " + ProfileManager.instance.GetPlayerName());
    }

    public void OnChangeToggleName(bool newStatus)
    {
        Debug.Log("cambio el estado " + newStatus);
    }

    public void OnClickPlay()
    {
        Debug.Log("jugar");
        SceneManager.LoadScene("LostInTheUniverse");
    }
}

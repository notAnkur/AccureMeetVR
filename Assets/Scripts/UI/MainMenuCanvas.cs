using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField]
    private InputField usernameInput;

    public void StartButton()
    {
        GLOBAL.SetUsername(usernameInput.text.ToString());
        SceneManager.LoadScene("Rooms");
    }
}

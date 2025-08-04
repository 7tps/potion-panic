using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TitleScreenButtonControl : MonoBehaviour
{
    public Button startGameButton;
    public Button keymapsButton;
    public Button settingsButton;
    public Button quitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        startGameButton.onClick.AddListener(startGame);
        keymapsButton.onClick.AddListener(showKeymaps);
        settingsButton.onClick.AddListener(settings);
        quitGameButton.onClick.AddListener(quitGame);
    }

    void startGame()
    {
        SceneManager.LoadScene("Level Selection");
    }

    void showKeymaps()
    {
        // set keymap subpanel to active
    }

    void settings()
    {
        // set setting subpanel to active
    }

    void quitGame()
    {
        // save the user's stats first
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

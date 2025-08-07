using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TitleScreenButtonControl : MonoBehaviour
{
    public Button startGameButton;
    public Button multiplayerButton;
    public Button keymapsButton;
    public Button settingsButton;
    public Button quitGameButton;

    public Button keymap_returnButton;

    public Button settings_returnButton;

    public GameObject keymapsPanel;
    public GameObject settingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        keymapsPanel.SetActive(false);
        settingsPanel.SetActive(false);

        startGameButton.onClick.AddListener(startGame);
        multiplayerButton.onClick.AddListener(startMP);
        keymapsButton.onClick.AddListener(showKeymaps);
        settingsButton.onClick.AddListener(showSettings);
        quitGameButton.onClick.AddListener(quitGame);

        keymap_returnButton.onClick.AddListener(closeKeymaps);

        settings_returnButton.onClick.AddListener(closeSettings);
    }

    void startMP()
    {
        SceneManager.LoadScene("MP Level Selection");
    }

    void startGame()
    {
        SceneManager.LoadScene("Level Selection");
    }

    void showKeymaps()
    {
        if (settingsPanel.activeInHierarchy)
            closeSettings();
        keymapsPanel.SetActive(true);
    }

    void closeKeymaps()
    {
        keymapsPanel.SetActive(false);
    }

    void showSettings()
    {
        if (keymapsPanel.activeInHierarchy)
            closeKeymaps();
        settingsPanel.SetActive(true);
    }

    void closeSettings()
    {
        settingsPanel.SetActive(false);
    }

    void quitGame()
    {
        // save the user's stats first
        Application.Quit();
        Debug.Log("QUIT GAME");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

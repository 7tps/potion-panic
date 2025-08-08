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

    public Button muteButton;
    public GameObject playImage;
    public GameObject muteImage;
    public int muteStatus = 0;

    // Start is called before the first frame update
    void Start()
    {
        keymapsPanel.SetActive(false);
        settingsPanel.SetActive(false);

        muteStatus = PlayerPrefs.GetInt("isAudioEnabled");
        if (muteStatus == 1)
        {
            showMuteImage();
        }
        else
        {
            showPlayImage();
        }

        startGameButton.onClick.AddListener(startGame);
        multiplayerButton.onClick.AddListener(startMP);
        keymapsButton.onClick.AddListener(showKeymaps);
        settingsButton.onClick.AddListener(showSettings);
        quitGameButton.onClick.AddListener(quitGame);
        muteButton.onClick.AddListener(toggleVolume);

        keymap_returnButton.onClick.AddListener(closeKeymaps);

        settings_returnButton.onClick.AddListener(closeSettings);

    }

    void toggleVolume()
    {
        if (muteStatus == 1)
        {
            MusicManager.Instance.VolumeOn();
            AudioManager.instance.VolumeOn();
            showPlayImage();
            muteStatus = 0;
            PlayerPrefs.SetInt("isAudioEnabled", muteStatus);
        }
        else
        {
            MusicManager.Instance.VolumeOff();
            AudioManager.instance.VolumeOff();
            showMuteImage();
            muteStatus = 1;
            PlayerPrefs.SetInt("isAudioEnabled", muteStatus);
        }
    }

    void showPlayImage()
    {
        playImage.SetActive(true);
        muteImage.SetActive(false);
    }

    void showMuteImage()
    {
        playImage.SetActive(false);
        muteImage.SetActive(true);
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
        muteButton.gameObject.SetActive(false);
        keymapsPanel.SetActive(true);
    }

    void closeKeymaps()
    {
        keymapsPanel.SetActive(false);
        muteButton.gameObject.SetActive(true);
    }

    void showSettings()
    {
        if (keymapsPanel.activeInHierarchy)
            closeKeymaps();
        muteButton.gameObject.SetActive(false);
        settingsPanel.SetActive(true);
    }

    void closeSettings()
    {
        settingsPanel.SetActive(false);
        muteButton.gameObject.SetActive(true);
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

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

    public Button keymap_returnButton;

    public Button settings_returnButton;

    public GameObject keymapsPanel;
    public GameObject settingsPanel;

    // Start of the popup implementation
    public GameObject popupPrefab;
    public GameObject parent;
    public static TitleScreenButtonControl instance;

    public string testTitle;
    public string testDescription;

    [ContextMenu("Test Notification")]
    public void TestPopup()
    {
        ShowPopupMenu(testTitle, testDescription);
    }

    public void ShowPopupMenu(string title, string description)
    {
        GameObject popup = Instantiate(popupPrefab, parent.transform);
        PopUpControl popup_ctrl = popup.GetComponent<PopUpControl>();
        popup_ctrl.AddText(title, description);
    }
    // end

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        startGameButton.onClick.AddListener(startGame);
        keymapsButton.onClick.AddListener(showKeymaps);
        settingsButton.onClick.AddListener(showSettings);
        quitGameButton.onClick.AddListener(quitGame);

        keymap_returnButton.onClick.AddListener(closeKeymaps);

        settings_returnButton.onClick.AddListener(closeSettings);
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

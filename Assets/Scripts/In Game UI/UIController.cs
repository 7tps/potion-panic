using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject canvas;

    public GameObject cutBarPrefab;
    private Dictionary<Ingredient, ProgressBar> cutBars = new Dictionary<Ingredient, ProgressBar>();
    private Dictionary<Ingredient, float> cutProgress = new Dictionary<Ingredient, float>();
    private Dictionary<Ingredient, float> totalCutTime = new Dictionary<Ingredient, float>();


    public GameObject pauseScreen;
    public Button quitLevelButton;

    public Button pausePlayButton;
    public GameObject pauseImage;
    public GameObject playImage;

    public GameObject failScreen;
    public Button backToMenuButton;
    public Button restartLevelButton;

    public GameObject finishGameScreen;
    public TMP_Text totalCustomerText;
    public TMP_Text finishScoreText;
    public Button playAgainButton;
    public Button finishBackToMenuButton;

    public int curLevel = 0;

    public TMP_Text scoreText;
    public int totalScore = 0;

    void loadLevelScreen()
    {
        SceneManager.LoadScene("Level Selection");
    }

    void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    void loadLevel2()
    {
        SceneManager.LoadScene("Level 2");
        Time.timeScale = 1;
    }

    void loadLevel3()
    {
        SceneManager.LoadScene("Level 3");
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        quitLevelButton.onClick.AddListener(loadLevelScreen);
        pausePlayButton.onClick.AddListener(switchSceneStatus);
        backToMenuButton.onClick.AddListener(loadLevelScreen);
        restartLevelButton.onClick.AddListener(restartLevel);
        if (curLevel == 1)
            playAgainButton.onClick.AddListener(loadLevel2);
        else if (curLevel == 2)
            playAgainButton.onClick.AddListener(loadLevel3);
        else if (curLevel == 3)
            playAgainButton.onClick.AddListener(restartLevel);
        finishBackToMenuButton.onClick.AddListener(loadLevelScreen);

        failScreen.SetActive(false);
        pauseScreen.SetActive(false);
        pauseImage.SetActive(true);
        playImage.SetActive(false);
        finishGameScreen.SetActive(false);

        scoreText.text = totalScore.ToString("D5");

        //Time.timeScale = 1;
    }

    void switchSceneStatus()
    {
        if (failScreen.activeSelf)
        {
            return;
        }
        if (!pauseScreen.activeInHierarchy)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            playImage.SetActive(true);
            pauseImage.SetActive(false);
        }
        else
        {
            pauseScreen.SetActive(false);
            playImage.SetActive(false);
            pauseImage.SetActive(true);
            Time.timeScale = 1;
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switchSceneStatus();
        }
    }

    public void setCutProgress(Ingredient ing, Vector3 position)
    {
        RecipeController.IngredientType type = ing.type;
        
        if (!cutBars.ContainsKey(ing))
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
            GameObject cutBarObj = Instantiate(cutBarPrefab, screenPosition, Quaternion.identity, canvas.transform);
            ProgressBar progressBar = cutBarObj.GetComponent<ProgressBar>();
            cutBars[ing] = progressBar;
            totalCutTime[ing] = RecipeController.instance.GetIngredientCutTime(type);
            cutProgress[ing] = totalCutTime[ing];
        }
        
        cutProgress[ing] -= Time.deltaTime;
        float progress = (totalCutTime[ing] - cutProgress[ing]) / totalCutTime[ing];
        cutBars[ing].UpdateProgress(progress);
        
        if (cutProgress[ing] <= 0)
        {
            Destroy(cutBars[ing].gameObject);
            cutBars.Remove(ing);
            cutProgress.Remove(ing);
            totalCutTime.Remove(ing);
        }
    }

    public void addScore(int score)
    {
        totalScore += score;
        scoreText.text = totalScore.ToString("D5");
        print("TOTAL SCORE UPDATED" + scoreText.text);
    }
}

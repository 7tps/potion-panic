using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void loadLevelScreen()
    {
        SceneManager.LoadScene("Level Selection");
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        quitLevelButton.onClick.AddListener(loadLevelScreen);
        pausePlayButton.onClick.AddListener(switchSceneStatus);
        pauseScreen.SetActive(false);
        pauseImage.SetActive(true);
        playImage.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
}

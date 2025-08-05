using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject canvas;

    public GameObject cutBarPrefab;
    private Dictionary<RecipeController.IngredientType, ProgressBar> cutBars = new Dictionary<RecipeController.IngredientType, ProgressBar>();
    private Dictionary<RecipeController.IngredientType, float> cutProgress = new Dictionary<RecipeController.IngredientType, float>();
    private Dictionary<RecipeController.IngredientType, float> totalCutTime = new Dictionary<RecipeController.IngredientType, float>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCutProgress(RecipeController.IngredientType type, Vector3 position)
    {
        if (!cutBars.ContainsKey(type))
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
            GameObject cutBarObj = Instantiate(cutBarPrefab, screenPosition, Quaternion.identity, canvas.transform);
            ProgressBar progressBar = cutBarObj.GetComponent<ProgressBar>();
            cutBars[type] = progressBar;
            totalCutTime[type] = RecipeController.instance.GetIngredientCutTime(type);
            cutProgress[type] = totalCutTime[type];
        }
        
        cutProgress[type] -= Time.deltaTime;
        float progress = (totalCutTime[type] - cutProgress[type]) / totalCutTime[type];
        cutBars[type].UpdateProgress(progress);
        
        if (cutProgress[type] <= 0)
        {
            Destroy(cutBars[type].gameObject);
            cutBars.Remove(type);
            cutProgress.Remove(type);
            totalCutTime.Remove(type);
        }
    }
}

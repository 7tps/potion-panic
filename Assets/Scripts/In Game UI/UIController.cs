using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject canvas;

    public GameObject cutBarPrefab;
    private Dictionary<Ingredient, ProgressBar> cutBars = new Dictionary<Ingredient, ProgressBar>();
    private Dictionary<Ingredient, float> cutProgress = new Dictionary<Ingredient, float>();
    private Dictionary<Ingredient, float> totalCutTime = new Dictionary<Ingredient, float>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
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

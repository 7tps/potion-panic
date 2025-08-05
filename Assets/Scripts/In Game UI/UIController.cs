using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject cutBarPrefab;
    public GameObject cutBar;

    public bool initialized = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCutProgress(RecipeController.IngredientType type, Transform position)
    {
        if (!initialized)
        {
            cutBar = Instantiate(cutBarPrefab, position);
            initialized = true;
        }

    }
}

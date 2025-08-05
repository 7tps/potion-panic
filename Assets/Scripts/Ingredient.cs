using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    public RecipeController.IngredientType type;

    public SpriteRenderer sr;
    
    public bool needToCut;
    public bool isCut = false;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        needToCut = RecipeController.instance.needToCut(type);
        Sprite sprite = RecipeController.instance.GetIngredientSprite(type);
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
    }
    
    public void InitializeWithType(RecipeController.IngredientType i)
    {
        Debug.Log("Initialized ingredient with type: " + i);
        needToCut = RecipeController.instance.needToCut(i);
        Sprite sprite = RecipeController.instance.GetIngredientSprite(i);
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
    }
}

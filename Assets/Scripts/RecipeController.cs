using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeController : MonoBehaviour
{

    public enum Ingredients
    {
        basil,
        ginger,
        garlic,
        parsnip,
        watermelon,
        avocado
    }

    public Sprite[] ingredientSprites;
    
    public static RecipeController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool needToCut(Ingredients ingredient)
    {
        if (ingredient == Ingredients.basil
            || ingredient == Ingredients.ginger
            || ingredient == Ingredients.garlic
            || ingredient == Ingredients.parsnip)
        {
            return false;
        }
        else 
        {
            return true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeController : MonoBehaviour
{

    public enum IngredientType
    {
        avocado,
        basil,
        garlic,
        ginger,
        parsnip,
        watermelon,
    }

    public Sprite[] ingredientSprites;
    public List<Recipe> validRecipes;
    
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

    public bool needToCut(IngredientType ingredient)
    {
        if (ingredient == IngredientType.basil
            || ingredient == IngredientType.ginger
            || ingredient == IngredientType.garlic
            || ingredient == IngredientType.parsnip)
        {
            return false;
        }
        else 
        {
            return true;
        }
    }

    public bool isRecipe(List<Ingredient> inputArray)
    {
        
        IngredientType[] ingredientsArray = new IngredientType[inputArray.Count];
        for (int i = 0; i < inputArray.Count; i++)
        {
            ingredientsArray[i] = inputArray[i].type;
        }
        
        if (ingredientsArray.Contains(IngredientType.avocado)
            && ingredientsArray.Contains(IngredientType.basil))
        {
            return true;
        }
        else if (ingredientsArray.Contains(IngredientType.ginger)
                 && ingredientsArray.Contains(IngredientType.garlic)
                 && ingredientsArray.Contains(IngredientType.basil))
        {
            return true;
        }
        else if (ingredientsArray.Contains(IngredientType.watermelon)
                 && ingredientsArray.Contains(IngredientType.parsnip)
                 && ingredientsArray.Contains(IngredientType.ginger))
        {
            return true;
        }
        else if (ingredientsArray.Contains(IngredientType.watermelon)
                 && ingredientsArray.Contains(IngredientType.avocado)
                 && ingredientsArray.Contains(IngredientType.ginger))
        {
            return true;
        }
        
        return false;
    }

    public bool isValidRecipe(List<Ingredient> ingredientsArray)
    {
        if (!isRecipe(ingredientsArray))
        {
            return false;
        }

        for (int i = 0; i < ingredientsArray.Count; i++)
        {
            Ingredient ing = ingredientsArray[i];
            if (needToCut(ing.type))
            {
                if (!ing.isCut)
                {
                    return false;
                }
            }
        }

        return true;
    }
}

[System.Serializable]
public class Recipe
{
    public List<Ingredient> ingredients;
    public float boilTime;
    
    public bool Equals(Recipe other)
    {
        return (ingredients.SequenceEqual(other.ingredients));
    }

    public Recipe(List<Ingredient> ingredients, float boilTime)
    {
        this.ingredients = ingredients;
        this.boilTime = boilTime;
    }
    
    /*
    public static override bool operator==(Object other)
    {
        return (ingredients.SequenceEqual(other.getIngredients()));
    }
    */
}
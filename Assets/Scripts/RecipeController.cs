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

    [System.Serializable]
    public class IngredientSpritePair
    {
        public IngredientType type;
        public Sprite sprite;
    }

    [SerializeField]
    public IngredientSpritePair[] ingredientSpritePairs;
    
    private Dictionary<IngredientType, Sprite> ingredientSprites = new Dictionary<IngredientType, Sprite>();
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
        // Convert array to dictionary for easy lookup
        if (ingredientSpritePairs != null)
        {
            foreach (var pair in ingredientSpritePairs)
            {
                if (pair.sprite != null)
                {
                    ingredientSprites[pair.type] = pair.sprite;
                }
            }
        }
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
        // Check against valid recipes
        foreach (Recipe recipe in validRecipes)
        {
            if (recipe.ingredients.Count == inputArray.Count)
            {
                bool match = true;
                for (int i = 0; i < inputArray.Count; i++)
                {
                    if (inputArray[i].type != recipe.ingredients[i].type)
                    {
                        match = false;
                        break;
                    }
                }
                if (match) return true;
            }
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

    public Sprite GetIngredientSprite(IngredientType type)
    {
        if (ingredientSprites.ContainsKey(type))
        {
            return ingredientSprites[type];
        }
        return null;
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
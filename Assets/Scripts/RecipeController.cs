using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeController : MonoBehaviour
{

    public enum IngredientType
    {
        emptyBottle,
        fullBottle,
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

    [System.Serializable]
    public class IngredientTimePair
    {
        public IngredientType type;
        public float cutTime;
    }

    [SerializeField]
    public IngredientSpritePair[] ingredientSpritePairs;
    [SerializeField]
    public IngredientSpritePair[] cutIngredientSpritePairs;
    [SerializeField]
    public IngredientTimePair[] ingredientTimePairs;
    
    private Dictionary<IngredientType, Sprite> ingredientSprites = new Dictionary<IngredientType, Sprite>();
    private Dictionary<IngredientType, Sprite> cutIngredientSprites = new Dictionary<IngredientType, Sprite>();
    private Dictionary<IngredientType, float> ingredientCutTime = new Dictionary<IngredientType, float>();
    [SerializeField]
    public List<Recipe> validRecipes = new List<Recipe>();
    
    public static RecipeController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        InitializeRecipes();
    }
    
    // Start is called before the first frame update
    void Start()
    {
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
        if (cutIngredientSpritePairs != null)
        {
            foreach (var pair in cutIngredientSpritePairs)
            {
                if (pair.sprite != null)
                {
                    cutIngredientSprites[pair.type] = pair.sprite;
                }
            }
        }
        if (ingredientTimePairs != null)
        {
            foreach (var pair in ingredientTimePairs)
            {
                ingredientCutTime[pair.type] = pair.cutTime;
            }
        }
    }

    void InitializeRecipes()
    {
        validRecipes = new List<Recipe>();
        
        Recipe recipe1 = new Recipe();
        recipe1.ingredientTypes = new List<IngredientType>();
        recipe1.ingredientTypes.Add(IngredientType.avocado);
        recipe1.ingredientTypes.Add(IngredientType.basil);
        recipe1.boilTime = 5.0f;
        recipe1.color = Recipe.RecipeColor.green;
        validRecipes.Add(recipe1);
        
        Recipe recipe2 = new Recipe();
        recipe2.ingredientTypes = new List<IngredientType>();
        recipe2.ingredientTypes.Add(IngredientType.basil);
        recipe2.ingredientTypes.Add(IngredientType.garlic);
        recipe2.ingredientTypes.Add(IngredientType.ginger);
        recipe2.boilTime = 8.0f;
        recipe2.color = Recipe.RecipeColor.orange;
        validRecipes.Add(recipe2);
        
        Recipe recipe3 = new Recipe();
        recipe3.ingredientTypes = new List<IngredientType>();
        recipe3.ingredientTypes.Add(IngredientType.ginger);
        recipe3.ingredientTypes.Add(IngredientType.parsnip);
        recipe3.ingredientTypes.Add(IngredientType.watermelon);
        recipe3.boilTime = 6.0f;
        recipe3.color = Recipe.RecipeColor.olive;
        validRecipes.Add(recipe3);
        
        Recipe recipe4 = new Recipe();
        recipe4.ingredientTypes = new List<IngredientType>();
        recipe4.ingredientTypes.Add(IngredientType.avocado);
        recipe4.ingredientTypes.Add(IngredientType.ginger);
        recipe4.ingredientTypes.Add(IngredientType.watermelon);
        recipe4.boilTime = 7.0f;
        recipe4.color = Recipe.RecipeColor.red;
        validRecipes.Add(recipe4);
    }

    public Recipe GetRecipeByColor(Recipe.RecipeColor color)
    {
        switch (color)
        {
            case Recipe.RecipeColor.green:
                return validRecipes[0];
            case Recipe.RecipeColor.orange:
                return validRecipes[1];
            case Recipe.RecipeColor.olive:
                return validRecipes[2];
            case Recipe.RecipeColor.red:
                return validRecipes[3];
            default:
                return null;
        }
    }

    public Recipe GetRandomRecipe()
    {
        int rand = Random.Range(0, validRecipes.Count);
        Debug.Log("Random Recipe Index: " + rand);
        return validRecipes[rand];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool needToCut(IngredientType ingredient)
    {
        if (ingredient == IngredientType.avocado
            || ingredient == IngredientType.watermelon)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public bool isRecipe(List<Ingredient> inputArray)
    {
        
        // Check against valid recipes
        foreach (Recipe recipe in validRecipes)
        {
            if (recipe.ingredientTypes.Count == inputArray.Count)
            {
                for (int i = 0; i < inputArray.Count; i++)
                {
                    if (inputArray[i].type != recipe.ingredientTypes[i])
                    {
                        //Debug.Log("Content ingredient: " + inputArray[i].type + " is not " + recipe.ingredientTypes[i]);
                        return false;
                    }
                }
                //Debug.Log("is recipe");
                return true;
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
                    //Debug.Log("Ingredient " + ing.type + " is not cut");
                    return false;
                }
            }
        }

        //Debug.Log("is valid recipe");
        return true;
    }

    public Recipe GetRecipe(List<Ingredient> inputArray)
    {
        foreach (Recipe recipe in validRecipes)
        {
            if (recipe.ingredientTypes.Count == inputArray.Count)
            {
                for (int i = 0; i < inputArray.Count; i++)
                {
                    if (inputArray[i].type != recipe.ingredientTypes[i])
                    {
                        Debug.Log("Content ingredient: " + inputArray[i].type + " is not " + recipe.ingredientTypes[i]);
                        break;
                    }
                }
                return recipe;
            }
        }
        Debug.Log("Invalid recipe.");
        return null;
    }
    
    public float GetBoilTime(List<Ingredient> ingredientsArray)
    {
        if (!isValidRecipe(ingredientsArray))
        {
            return -1;
        }

        foreach (Recipe recipe in validRecipes)
        {
            if (recipe.ingredientTypes.Count == ingredientsArray.Count)
            {
                for (int i = 0; i < ingredientsArray.Count; i++)
                {
                    if (ingredientsArray[i].type != recipe.ingredientTypes[i])
                    {
                        return -1;
                    }
                }
                return recipe.boilTime;
            }
        }

        return -1;
    }

    public Sprite GetIngredientSprite(IngredientType type)
    {
        if (ingredientSprites.ContainsKey(type))
        {
            return ingredientSprites[type];
        }
        return null;
    }
    
    public Sprite GetCutIngredientSprite(IngredientType type)
    {
        if (cutIngredientSprites.ContainsKey(type))
        {
            return cutIngredientSprites[type];
        }
        return null;
    }

    public float GetIngredientCutTime(IngredientType type)
    {
        if (ingredientCutTime.ContainsKey(type))
        {
            return ingredientCutTime[type];
        }
        return 0f;
    }
}

[System.Serializable]
public class Recipe
{

    public enum RecipeColor
    {
        red,
        brown,
        orange,
        green, 
        olive,
        gray,
        darkblue,
        teal,
        blue,
        purple,
        pink,
        darkpurple,
        black,
        white,
        empty
    }
    
    public List<RecipeController.IngredientType> ingredientTypes;
    public float boilTime;
    public RecipeColor color;
    
    public bool Equals(Recipe other)
    {
        if (ingredientTypes.Count != other.ingredientTypes.Count)
        {
            return false;
        }
        for (int i = 0; i < ingredientTypes.Count; i++)
        {
            if (ingredientTypes[i] != other.ingredientTypes[i])
            {
                return false;
            }
        }

        return true;
    }

    public Recipe()
    {
        ingredientTypes = new List<RecipeController.IngredientType>();
    }
    
    public Recipe(List<RecipeController.IngredientType> types, float boilTime, RecipeColor color)
    {
        this.ingredientTypes = types;
        this.boilTime = boilTime;
        this.color = color;
    }
}
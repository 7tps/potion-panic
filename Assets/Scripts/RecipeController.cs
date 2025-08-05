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
        
        List<Ingredient> recipe1Ingredients = new List<Ingredient>();
        recipe1Ingredients.Add(CreateIngredient(IngredientType.avocado));
        recipe1Ingredients.Add(CreateIngredient(IngredientType.basil));
        Recipe recipe1 = new Recipe(recipe1Ingredients, 5.0f);
        validRecipes.Add(recipe1);
        
        List<Ingredient> recipe2Ingredients = new List<Ingredient>();
        recipe2Ingredients.Add(CreateIngredient(IngredientType.ginger));
        recipe2Ingredients.Add(CreateIngredient(IngredientType.garlic));
        recipe2Ingredients.Add(CreateIngredient(IngredientType.basil));
        Recipe recipe2 = new Recipe(recipe2Ingredients, 8.0f);
        validRecipes.Add(recipe2);
        
        List<Ingredient> recipe3Ingredients = new List<Ingredient>();
        recipe3Ingredients.Add(CreateIngredient(IngredientType.watermelon));
        recipe3Ingredients.Add(CreateIngredient(IngredientType.parsnip));
        recipe3Ingredients.Add(CreateIngredient(IngredientType.ginger));
        Recipe recipe3 = new Recipe(recipe3Ingredients, 6.0f);
        validRecipes.Add(recipe3);
        
        List<Ingredient> recipe4Ingredients = new List<Ingredient>();
        recipe4Ingredients.Add(CreateIngredient(IngredientType.watermelon));
        recipe4Ingredients.Add(CreateIngredient(IngredientType.avocado));
        recipe4Ingredients.Add(CreateIngredient(IngredientType.ginger));
        Recipe recipe4 = new Recipe(recipe4Ingredients, 7.0f);
        validRecipes.Add(recipe4);
    }

    Ingredient CreateIngredient(IngredientType type)
    {
        GameObject tempGO = new GameObject("TempIngredient");
        Ingredient ingredient = tempGO.AddComponent<Ingredient>();
        ingredient.RecipeInitialize(type);
        return ingredient;
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
                for (int i = 0; i < inputArray.Count; i++)
                {
                    if (inputArray[i].type != recipe.ingredients[i].type)
                    {
                        return false;
                    }
                }
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
    public List<Ingredient> ingredients;
    public float boilTime;
    
    public bool Equals(Recipe other)
    {
        if (ingredients.Count != other.ingredients.Count)
        {
            return false;
        }
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].type != other.ingredients[i].type)
            {
                return false;
            }
        }

        return true;
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
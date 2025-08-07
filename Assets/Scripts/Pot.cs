using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Pot : MonoBehaviour
{

    [System.Serializable]
    public class RecipeColorSpritePair
    {
        public Recipe.RecipeColor color;
        public Sprite sprite;
    }
    
    [SerializeField]
    public RecipeColorSpritePair[] recipeColorSpritePairs;
    
    private Dictionary<Recipe.RecipeColor, Sprite> recipeSprites = new Dictionary<Recipe.RecipeColor, Sprite>();
    
    public List<Ingredient> contents;
    public Recipe.RecipeColor contentColor = Recipe.RecipeColor.empty;
    public Recipe.RecipeColor lastRecipeResultColor = Recipe.RecipeColor.empty;

    public Vector2Int[] gridPositions;

    public SpriteRenderer sr;
    
    public bool isBoiling = false;
    public bool readyToCollect = false;
    public float boilProgress;
    public float boilTime;

    public Image progressStatus;
    public GameObject progressBar;

    [Header("Valid Orders")] 
    public bool isCold = false;
    public GameObject coldIngredientPrefab;
    public List<Recipe.RecipeColor> validRecipes;

    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log($"[{gameObject.name}] Pot Start() called - isCold: {isCold}");
        
        sr = GetComponent<SpriteRenderer>();
        
        if (recipeColorSpritePairs != null)
        {
            //Debug.Log($"[{gameObject.name}] Setting up recipe sprites - count: {recipeColorSpritePairs.Length}");
            foreach (var pair in recipeColorSpritePairs)
            {
                if (pair.sprite != null)
                {
                    recipeSprites[pair.color] = pair.sprite;
                    //Debug.Log($"[{gameObject.name}] Added sprite for color: {pair.color}");
                }
                else
                {
                    Debug.LogWarning($"[{gameObject.name}] Null sprite for color: {pair.color}");
                }
            }
        }
        else
        {
            Debug.LogWarning($"[{gameObject.name}] recipeColorSpritePairs is null!");
        }
        
        // Initialize validRecipes list if it's null
        if (validRecipes == null)
        {
            validRecipes = new List<Recipe.RecipeColor>();
            //Debug.Log($"[{gameObject.name}] Initialized validRecipes list");
        }
        
        //Debug.Log($"[{gameObject.name}] Before adding recipes - validRecipes count: {validRecipes.Count}");
        
        switch (isCold)
        {
            case false:
                //Debug.Log($"[{gameObject.name}] Adding hot recipes (0-13)");
                for (int i = 0; i < 14; i++)
                {
                    validRecipes.Add((Recipe.RecipeColor) i);
                    //Debug.Log($"[{gameObject.name}] Added hot recipe: {(Recipe.RecipeColor)i}");
                }
                break;
            case true:
                //Debug.Log($"[{gameObject.name}] Adding cold recipes (15-28)");
                for (int i = 15; i < 29; i++)
                {
                    validRecipes.Add((Recipe.RecipeColor) i);
                    //Debug.Log($"[{gameObject.name}] Added cold recipe: {(Recipe.RecipeColor)i}");
                }
                break;
        }
        
        //Debug.Log($"[{gameObject.name}] After adding recipes - validRecipes count: {validRecipes.Count}");
        
        ClearAllIngredients();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (CanBoil() && !isBoiling)
        {
            Debug.Log($"[{gameObject.name}] started boiling");
            StartBoiling();
        }
        */

        
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (CanBoil() && !isBoiling)
            {
                Debug.Log($"[{gameObject.name}] started boiling");
                StartBoiling();
            }
        }
        

        if (isBoiling)
        {
            UpdateBoiling();
        }
    }

    public bool CanBoil()
    {
        bool isValidRecipe = RecipeController.instance.isValidRecipe(contents);
        Recipe.RecipeColor recipeColor = Recipe.RecipeColor.empty;
        bool isValidColor = false;
        
        if (isValidRecipe)
        {
            recipeColor = RecipeController.instance.GetRecipeColor(contents);
            isValidColor = validRecipes.Contains(recipeColor);
        }
        
        /*
        Debug.Log($"[{gameObject.name}] CanBoil() check:");
        Debug.Log($"[{gameObject.name}]   - isValidRecipe: {isValidRecipe}");
        Debug.Log($"[{gameObject.name}]   - recipeColor: {recipeColor}");
        Debug.Log($"[{gameObject.name}]   - isValidColor: {isValidColor}");
        Debug.Log($"[{gameObject.name}]   - validRecipes count: {validRecipes.Count}");
        Debug.Log($"[{gameObject.name}]   - contents count: {contents.Count}");
        */
        
        if (contents.Count > 0)
        {
            Debug.Log($"[{gameObject.name}]   - Contents:");
            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i] != null)
                {
                    Debug.Log($"[{gameObject.name}]     [{i}]: {contents[i].type}");
                }
                else
                {
                    Debug.LogWarning($"[{gameObject.name}]     [{i}]: NULL ingredient!");
                }
            }
        }
        
        return isValidRecipe && isValidColor;
    }

    public void AddIngredient(Ingredient ing)
    {
        Debug.Log($"[{gameObject.name}] AddIngredient() called with: {ing.type}");
        
        if (ing == null)
        {
            Debug.LogError($"[{gameObject.name}] Attempted to add null ingredient!");
            return;
        }
        
        contents.Add(ing);
        contents = contents.OrderBy(x => x.type.ToString()).ToList();
        
        Debug.Log($"[{gameObject.name}] After adding ingredient - contents count: {contents.Count}");
        for (int i = 0; i < contents.Count; i++)
        {
            if (contents[i] != null)
            {
                Debug.Log($"[{gameObject.name}]   [{i}]: {contents[i].type}");
            }
            else
            {
                Debug.LogWarning($"[{gameObject.name}]   [{i}]: NULL ingredient!");
            }
        }
        
        ing.gameObject.SetActive(false);
    }
    
    public void StartBoiling()
    {
        Debug.Log($"[{gameObject.name}] StartBoiling() called");
        
        boilTime = RecipeController.instance.GetBoilTime(contents);
        boilProgress = boilTime;
        isBoiling = true;
        
        Debug.Log($"[{gameObject.name}] boilTime is " + boilTime);
        Debug.Log($"[{gameObject.name}] boilProgress is " + boilProgress);
        
        if (progressBar != null)
        {
            progressBar.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"[{gameObject.name}] progressBar is null!");
        }
        
        if (progressStatus != null)
        {
            progressStatus.fillAmount = 0;
        }
        else
        {
            Debug.LogWarning($"[{gameObject.name}] progressStatus is null!");
        }
    }
    
    public void UpdateBoiling()
    {
        boilProgress -= Time.deltaTime;
        
        if (progressStatus != null)
        {
            progressStatus.fillAmount = 1 - boilProgress / boilTime;
        }
        
        if (boilProgress <= 0)
        {
            Debug.Log($"[{gameObject.name}] Boiling finished");
            isBoiling = false;
            List<Ingredient> recipeCheck = new List<Ingredient>();
            for (int i = 0; i < contents.Count; i++)
            {
                recipeCheck.Add(contents[i]);
            }
            ClearAllIngredients();
            
            if (progressBar != null)
            {
                progressBar.SetActive(false);
            }
            
            EndBoiling(recipeCheck);
        }
    }

    public void EndBoiling(List<Ingredient> recipeCheck)
    {
        Debug.Log($"[{gameObject.name}] EndBoiling() called with {recipeCheck.Count} ingredients");
        
        boilTime = 0;
        boilProgress = 0;
        Recipe r = RecipeController.instance.GetRecipe(recipeCheck);
        
        if (r == null)
        {
            Debug.LogError($"[{gameObject.name}] Recipe not found for ingredients:");
            for (int i = 0; i < recipeCheck.Count; i++)
            {
                if (recipeCheck[i] != null)
                {
                    Debug.LogError($"[{gameObject.name}]   [{i}]: {recipeCheck[i].type}");
                }
                else
                {
                    Debug.LogError($"[{gameObject.name}]   [{i}]: NULL ingredient!");
                }
            }
            return;
        }
        
        Debug.Log($"[{gameObject.name}] Found recipe with color: {r.color}");
        
        contentColor = r.color;
        lastRecipeResultColor = r.color;
        Sprite s = GetRecipeSprite(r.color);
        
        if (s == null)
        {
            Debug.LogError($"[{gameObject.name}] No recipe sprite found for color: {r.color}");
            return;
        }
        
        sr.sprite = s;
        readyToCollect = true;
        Debug.Log($"[{gameObject.name}] Pot is ready to collect!");
    }

    public bool CollectPotion()
    {
        Debug.Log($"[{gameObject.name}] CollectPotion() called - readyToCollect: {readyToCollect}");
        
        if (!readyToCollect)
        {
            return false;
        }
        
        readyToCollect = false;
        contentColor = Recipe.RecipeColor.empty;
        Sprite s = GetRecipeSprite(contentColor);
        sr.sprite = s;
        ClearAllIngredients();
        return true;
    }

    private void ClearAllIngredients()
    {
        Debug.Log($"[{gameObject.name}] ClearAllIngredients() called - contents count: {contents.Count}");
        
        for (int i = 0; i < contents.Count; i++)
        {
            if (contents[i] != null)
            {
                Destroy(contents[i].gameObject);
            }
        }
        contents.Clear();
        
        if (isCold)
        {
            Debug.Log($"[{gameObject.name}] Spawning cold ingredient");
            if (coldIngredientPrefab != null)
            {
                AddIngredient(SpawnColdIngredient());
                Debug.Log($"[{gameObject.name}] Cold ingredient spawned successfully");
            }
            else
            {
                Debug.LogError($"[{gameObject.name}] coldIngredientPrefab is null!");
            }
        }
        
        Debug.Log($"[{gameObject.name}] After clearing - contents count: {contents.Count}");
    }

    private Ingredient SpawnColdIngredient()
    {
        Debug.Log($"[{gameObject.name}] SpawnColdIngredient() called");
        
        if (coldIngredientPrefab == null)
        {
            Debug.LogError($"[{gameObject.name}] coldIngredientPrefab is null!");
            return null;
        }
        
        GameObject g = Instantiate(coldIngredientPrefab, transform.position, Quaternion.identity);
        Ingredient i = g.GetComponent<Ingredient>();
        
        if (i == null)
        {
            Debug.LogError($"[{gameObject.name}] Failed to get Ingredient component from coldIngredientPrefab!");
            return null;
        }
        
        i.InitializeWithType(RecipeController.IngredientType.coldModifier);
        g.transform.localScale = Vector3.one;
        g.SetActive(false);
        
        Debug.Log($"[{gameObject.name}] Cold ingredient spawned with type: {i.type}");
        return i;
    }

    public Sprite GetRecipeSprite(Recipe.RecipeColor type)
    {
        if (recipeSprites.ContainsKey(type))
        {
            return recipeSprites[type];
        }
        
        Debug.LogWarning($"[{gameObject.name}] No sprite found for recipe color: {type}");
        return null;
    }
    
    public bool IsAtGridPosition(Vector2Int pos)
    {
        for (int i = 0; i < gridPositions.Length; i++)
        {
            if (gridPositions[i] == pos)
            {
                return true;
            }
        }

        return false;
    }

    
}

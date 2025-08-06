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

    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
        if (recipeColorSpritePairs != null)
        {
            foreach (var pair in recipeColorSpritePairs)
            {
                if (pair.sprite != null)
                {
                    recipeSprites[pair.color] = pair.sprite;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RecipeController.instance.isValidRecipe(contents) && !isBoiling)
        {
            Debug.Log("started boiling");
            StartBoiling();
        }

        /*
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Checking if recipe is valid...");
            RecipeController.instance.isValidRecipe(contents);
            StartBoiling();
        }
        */

        if (isBoiling)
        {
            UpdateBoiling();
        }
    }

    public void AddIngredient(Ingredient ing)
    {
        contents.Add(ing);
        contents = contents.OrderBy(x => x.type.ToString()).ToList();
        for (int i = 0; i < contents.Count; i++)
        {
            Debug.Log(contents[i].type.ToString());
        }
        ing.gameObject.SetActive(false);
    }
    
    public void StartBoiling()
    {
        boilTime = RecipeController.instance.GetBoilTime(contents);
        boilProgress = boilTime;
        isBoiling = true;
        Debug.Log("boilTime is " + boilTime);
        Debug.Log("boilProgress is " + boilProgress);
        progressBar.SetActive(true);
        progressStatus.fillAmount = 0;
    }
    
    public void UpdateBoiling()
    {
        boilProgress -= Time.deltaTime;
        progressStatus.fillAmount = 1 - boilProgress / boilTime;
        if (boilProgress <= 0)
        {
            isBoiling = false;
            List<Ingredient> recipeCheck = new List<Ingredient>();
            for (int i = 0; i < contents.Count; i++)
            {
                recipeCheck.Add(contents[i]);
            }
            contents.Clear();
            progressBar.SetActive(false);
            EndBoiling(recipeCheck);
        }
    }

    public void EndBoiling(List<Ingredient> recipeCheck)
    {
        boilTime = 0;
        boilProgress = 0;
        Recipe r = RecipeController.instance.GetRecipe(recipeCheck);
        if (r == null)
        {
            Debug.Log("Recipe not found.");
            return;
        }
        
        contentColor = r.color;
        lastRecipeResultColor = r.color;
        Sprite s = GetRecipeSprite(r.color);
        if (s == null)
        {
            Debug.Log("No recipe sprite found...");
            return;
        }
        sr.sprite = s;
        readyToCollect = true;
    }

    public bool CollectPotion()
    {
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
        for (int i = 0; i < contents.Count; i++)
        {
            Destroy(contents[i].gameObject);
        }
        contents.Clear();
    }

    public Sprite GetRecipeSprite(Recipe.RecipeColor type)
    {
        if (recipeSprites.ContainsKey(type))
        {
            return recipeSprites[type];
        }
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

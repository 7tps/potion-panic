using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pot : MonoBehaviour
{

    public List<Ingredient> contents;

    public Vector2Int[] gridPositions;

    public SpriteRenderer sr;
    
    public bool isBoiling = false;
    public float boilProgress;
    public float boilTime;

    public class RecipeColorSpritePair
    {
        public Recipe.RecipeColor color;
        public Sprite sprite;
    }
    
    [SerializeField]
    public RecipeColorSpritePair[] ingredientTimePairs;
    
    private Dictionary<Recipe.RecipeColor, Sprite> recipeSprites = new Dictionary<Recipe.RecipeColor, Sprite>();

    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
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
    }
    
    public void UpdateBoiling()
    {
        boilProgress -= Time.deltaTime;
        if (boilProgress <= 0)
        {
            isBoiling = false;
            EndBoiling();
        }
    }

    public void EndBoiling()
    {
        Recipe r = RecipeController.instance.GetRecipe(contents);
        if (r == null)
        {
            Debug.Log("Recipe not found.");
            return;
        }
        Sprite s = GetRecipeSprite(r.color);
        if (s == null)
        {
            Debug.Log("No recipe sprite found...");
            return;
        }
        sr.sprite = s;
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

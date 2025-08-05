using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pot : MonoBehaviour
{

    public List<Ingredient> contents;

    public Vector2Int[] gridPositions;

    public bool isBoiling = false;
    public float boilProgress;
    public float boilTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        contents.Clear();
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

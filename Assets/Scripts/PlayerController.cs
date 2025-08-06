using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D rb;
    public bool movementDisabled;
    public float moveSpeed = 10;

    public float movementX;
    public float movementY;
    
    private Vector2 lastDirection = Vector2.zero;

    public Vector2Int lookingAtGridBlock = Vector2Int.zero;
    
    private Transform ingredientPosition;
    private Ingredient heldIngredient;

    public GameObject progressBarPrefab;
    
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
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ingredientPosition = transform.Find("IngredientPosition");
    }

    void Update()
    {
        HandleInput();
        HandleInteraction();
    }

    void HandleInput()
    {
        
        if (movementDisabled)
        {
            return;
        }

        float[] movementInput = new float[2];
        movementInput[0] = Input.GetAxisRaw("Horizontal");
        movementInput[1] = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(movementInput[0], movementInput[1]).normalized;
        rb.velocity = movement * moveSpeed;
        
        if (movement.magnitude > 0.1f)
        {
            lastDirection = movement;
        }

        lookingAtGridBlock = GetLookingAtGridBlock();
    }

    void HandleInteraction()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Counter[] counters = FindObjectsOfType<Counter>();
            foreach (Counter counter in counters)
            {
                if (counter.gridPosition == lookingAtGridBlock && counter.hasIngredient)
                {
                    Ingredient ingredient = counter.heldIngredient;
                    Vector3 progressBarPosition = ingredient.transform.position + Vector3.up * 0.7f;
                    //Instantiate(progressBarPrefab, progressBarPosition, Quaternion.identity);
                    if (ingredient != null && ingredient.needToCut && !ingredient.isCut)
                    {
                        //UI
                        UIController.instance.setCutProgress(ingredient, progressBarPosition);
                        //UI
                        ingredient.cutIngredient();
                        if (ingredient.cutProgress <= 0)
                        {
                            ingredient.isCut = true;
                        }
                        return;
                    }
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (heldIngredient != null)
            {
                Pot[] pots = FindObjectsOfType<Pot>();
                foreach (Pot pot in pots)
                {
                    if (pot.IsAtGridPosition(lookingAtGridBlock))
                    {
                        if (heldIngredient.type == RecipeController.IngredientType.emptyBottle)
                        {
                            pot.CollectPotion();
                            heldIngredient.type = RecipeController.IngredientType.fullBottle;
                            heldIngredient.color = pot.contentColor;
                            return;
                        }
                        pot.AddIngredient(heldIngredient);
                        heldIngredient = null;
                        return;
                    }
                }
            }
            
            if (heldIngredient != null)
            {
                if (CustomerSpawner.instance.canPlayerSubmit && heldIngredient.type == RecipeController.IngredientType.fullBottle)
                {
                    if (CustomerSpawner.instance.SubmitOrder(heldIngredient))
                    {
                        Destroy(heldIngredient.gameObject);
                        heldIngredient = null;
                    }
                }
            }
            
            if (heldIngredient != null)
            {
                Counter[] counters = FindObjectsOfType<Counter>();
                foreach (Counter counter in counters)
                {
                    if (counter.gridPosition == lookingAtGridBlock && !counter.hasIngredient)
                    {
                        counter.PlaceIngredient(heldIngredient);
                        heldIngredient = null;
                        return;
                    }
                }
            }
            
            if (heldIngredient == null)
            {
                Counter[] counters = FindObjectsOfType<Counter>();
                foreach (Counter counter in counters)
                {
                    if (counter.gridPosition == lookingAtGridBlock && counter.hasIngredient)
                    {
                        heldIngredient = counter.RemoveIngredient();
                        heldIngredient.transform.SetParent(ingredientPosition);
                        heldIngredient.transform.localPosition = Vector3.zero;
                        return;
                    }
                }
            }
            
            Ingredient[] ingredients = FindObjectsOfType<Ingredient>();
            foreach (Ingredient ingredient in ingredients)
            {
                Vector2Int ingredientGridPos = new Vector2Int(
                    Mathf.FloorToInt(ingredient.transform.position.x + 1f),
                    Mathf.FloorToInt(ingredient.transform.position.y + 0.5f)
                );
                
                if (ingredientGridPos == lookingAtGridBlock && heldIngredient == null)
                {
                    heldIngredient = ingredient;
                    ingredient.transform.SetParent(ingredientPosition);
                    ingredient.transform.localPosition = Vector3.zero;
                    
                    IngredientCrate[] crates = FindObjectsOfType<IngredientCrate>();
                    foreach (IngredientCrate crate in crates)
                    {
                        if (crate.gridPosition == ingredientGridPos)
                        {
                            crate.SetHasIngredient(false);
                            break;
                        }
                    }
                    
                    return;
                }
            }
            
            if (heldIngredient == null)
            {
                IngredientCrate[] crates = FindObjectsOfType<IngredientCrate>();
                foreach (IngredientCrate crate in crates)
                {
                    if (crate.gridPosition == lookingAtGridBlock && !crate.hasIngredient)
                    {
                        Debug.Log("spawned ingredient");
                        if (crate.type == RecipeController.IngredientType.emptyBottle)
                        {
                            Debug.Log("spawned bottle");
                            Ingredient i = crate.spawnIngredient();
                            crate.SetHasIngredient(false);
                            heldIngredient = i;
                            i.transform.SetParent(ingredientPosition);
                            i.transform.localPosition = Vector3.zero;
                            break;
                        }
                        crate.spawnIngredient();
                        break;
                    }
                }
            }
        }
    }

    public Vector2Int GetLookingAtGridBlock()
    {
        Vector2Int currentGridPos = new Vector2Int(
            Mathf.FloorToInt(transform.position.x + 1f),
            Mathf.FloorToInt(transform.position.y + 0.5f)
        );

        if (lastDirection.magnitude > 0.1f)
        {
            Vector2Int lookDirection = new Vector2Int(
                Mathf.RoundToInt(lastDirection.x),
                Mathf.RoundToInt(lastDirection.y)
            );
            
            return currentGridPos + lookDirection;
        }
        
        return currentGridPos;
    }
    
}

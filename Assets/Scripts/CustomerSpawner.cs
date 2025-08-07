using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerSpawner : MonoBehaviour
{

    [SerializeField]
    public RecipeColorSpritePair[] recipeColorSpritePairs;
    
    [Header("Customer Settings")]
    public Transform[] customerPositions;
    public Sprite[] customerSprites;
    public Order[] customerOrders;
    public GameObject[] thinkingBubbles;
    public GameObject[] progressBars;
    public Customer[] instantiatedCustomers;
    public GameObject customerPrefab;
    
    [Header("Spawn Settings")]
    public float minSpawnInterval = 3f;
    public float maxSpawnInterval = 8f;
    public bool autoSpawn = true;
    [SerializeField]
    private float nextSpawnTime;
    public float spawnTimeInterval = 15f;

    [Header("Order Submission")]
    public bool canPlayerSubmit = false;

    [Header("Valid Orders")] 
    public List<Recipe.RecipeColor> validRecipes;

    public int customerIndex = -1;

    [System.Serializable]
    public class RecipeColorSpritePair
    {
        public Recipe.RecipeColor color;
        public Sprite sprite;
    }
    
    private Dictionary<Recipe.RecipeColor, Sprite> potionSprites = new Dictionary<Recipe.RecipeColor, Sprite>();
    
    public static CustomerSpawner instance;

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
        if (recipeColorSpritePairs != null)
        {
            foreach (var pair in recipeColorSpritePairs)
            {
                if (pair.sprite != null)
                {
                    potionSprites[pair.color] = pair.sprite;
                }
            }
        }

        for (int i = 0; i < customerOrders.Length; i++)
        {
            customerOrders[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < thinkingBubbles.Length; i++)
        {
            thinkingBubbles[i].SetActive(false);
        }
        for (int i = 0; i < progressBars.Length; i++)
        {
            progressBars[i].SetActive(false);
        }
        
        if (instantiatedCustomers == null || instantiatedCustomers.Length != customerPositions.Length)
        {
            instantiatedCustomers = new Customer[customerPositions.Length];
        }
        
        if (autoSpawn)
        {
            SetNextSpawnTime();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (autoSpawn && Time.time >= nextSpawnTime)
        {
            bool spawned = SpawnCustomer();
            SetNextSpawnTime();
            Debug.Log("Next spawn time set to: " + nextSpawnTime);
        }
    }
    
    void SetNextSpawnTime()
    {
        //nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        nextSpawnTime = Time.time + spawnTimeInterval;
    }
    
    public bool SpawnCustomer()
    {
        if (customerPositions.Length == 0 || customerPrefab == null)
        {
            Debug.LogWarning("No customer positions or prefab set!");
            return false;
        }
        
        if (instantiatedCustomers == null || instantiatedCustomers.Length != customerPositions.Length)
        {
            Debug.LogWarning("instantiatedCustomers array not properly initialized!");
            return false;
        }
        
        int index = Random.Range(0, customerPositions.Length);
        if (index >= instantiatedCustomers.Length)
        {
            Debug.LogWarning("Index out of bounds: " + index + " >= " + instantiatedCustomers.Length);
            return false;
        }
        
        if (instantiatedCustomers[index] != null)
        {
            Debug.Log("Position " + index + " is already occupied!");
            return false;
        }
        
        Transform spawnPosition = customerPositions[index];
        GameObject customerObj = Instantiate(customerPrefab, spawnPosition.position, spawnPosition.rotation);
        Order order = customerOrders[index];
        GameObject bubble = thinkingBubbles[index];
        GameObject progressBar = progressBars[index];
        Customer customer = customerObj.GetComponent<Customer>();
        instantiatedCustomers[index] = customer;
        
        if (customer != null)
        {
            Recipe.RecipeColor randomColor = GetRandomRecipeColor();
            Sprite customerSprite = customerSprites[Random.Range(0, customerSprites.Length)];
            Sprite potionSprite = GetPotionSprite(randomColor);
            order.SetSprite(potionSprite);
            order.color = randomColor;

            customer.Initialize(
                customerSprite,
                order,
                bubble,
                progressBar.GetComponent<ProgressBar>(),
                Random.Range(20f, 30f)
            );
            return true;
        }
        return false;
    }

    public bool SubmitOrder(Ingredient bottle)
    {
        if (instantiatedCustomers[customerIndex].order.color == bottle.color)
        {
            Debug.Log("Submitted order to: customer " + customerIndex);
            Customer c = instantiatedCustomers[customerIndex];
            instantiatedCustomers[customerIndex] = null;
            c.order.gameObject.SetActive(false);
            c.progress.gameObject.SetActive(false);
            Debug.Log(c.GetScore()); //add UI score
            Destroy(c.gameObject);
            return true;
        }

        return false;
    }
    
    Recipe.RecipeColor GetRandomRecipeColor()
    {
        return validRecipes[Random.Range(0, validRecipes.Count)];
        //return RecipeController.instance.GetRandomRecipe().color;
    }
    
    public Sprite GetPotionSprite(Recipe.RecipeColor color)
    {
        if (potionSprites.ContainsKey(color))
        {
            return potionSprites[color];
        }
        return null;
    }
}

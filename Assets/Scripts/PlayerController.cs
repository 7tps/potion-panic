using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D rb;
    public bool movementDisabled;
    public float moveSpeed = 10;

    public float movementX;
    public float movementY;
    
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
    }

    void Update()
    {
        HandleInput();
        
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
        
    }
    
}
